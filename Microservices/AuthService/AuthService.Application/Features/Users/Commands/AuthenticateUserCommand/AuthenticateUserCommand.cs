namespace AuthService.Application.Features.Users.Commands.AuthenticateUser;

using Common.Exceptions;
using Common.Wrappers;
using AutoMapper;
using AuthService.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Application.Features.SharedViewModels;
using System.Text;
using Microsoft.Extensions.Options;
using AuthService.Application.Settings;

public class AuthenticateUserCommand : IRequest<Response<AuthenticationResponse>>
{
  [Required]
  [EmailAddress]
  public string Email { get; set; }

  [Required]
  [MinLength(6)]
  public string Password { get; set; }
}

public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, Response<AuthenticationResponse>>
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly SignInManager<ApplicationUser> _signInManager;
  private readonly JWTSettings _jwtSettings;
  private readonly IMapper _mapper;
  public AuthenticateUserCommandHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JWTSettings> jwtSettings, IMapper mapper)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _jwtSettings = jwtSettings.Value;
    _mapper = mapper;
  }

  public async Task<Response<AuthenticationResponse>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
  {
    var user = await _userManager.FindByEmailAsync(request.Email);
    if (user == null)
    {
      throw new ApiException($"No Accounts Registered with {request.Email}.");
    }

    var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
    if (!result.Succeeded)
    {
      throw new ApiException($"Invalid Credentials for '{request.Email}'.");
    }

    JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
    AuthenticationResponse response = new AuthenticationResponse();
    response.Id = user.Id;
    response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    response.Email = user.Email;
    response.Expires = jwtSecurityToken.Payload.Exp;

    var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
    response.Roles = rolesList.ToList();

    return new Response<AuthenticationResponse>(response, $"Authenticated {user.UserName}");
  }

  private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
  {
    var userClaims = await _userManager.GetClaimsAsync(user);
    var roles = await _userManager.GetRolesAsync(user);

    var roleClaims = new List<Claim>();

    for (int i = 0; i < roles.Count; i++)
    {
      roleClaims.Add(new Claim("roles", roles[i]));
    }

    var claims = new[]
    {
      new Claim(JwtRegisteredClaimNames.Sub, user.Id),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
      new Claim(JwtRegisteredClaimNames.Email, user.Email),
    }
    .Union(userClaims)
    .Union(roleClaims);

    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

    var jwtSecurityToken = new JwtSecurityToken(
        issuer: _jwtSettings.Issuer,
        audience: _jwtSettings.Audience,
        claims: claims,
        expires: DateTime.UtcNow.AddDays(_jwtSettings.DurationInDays),
        signingCredentials: signingCredentials
     );

    return jwtSecurityToken;
  }
}
