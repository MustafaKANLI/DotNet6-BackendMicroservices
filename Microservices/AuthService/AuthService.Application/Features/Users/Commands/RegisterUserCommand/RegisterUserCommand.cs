namespace AuthService.Application.Features.Users.Commands.RegisterUser;

using Common.Exceptions;
using Common.Wrappers;
using AutoMapper;
using AuthService.Domain.Entities;
using AuthService.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class RegisterUserCommand : IRequest<Response<string>>
{
  [Required]
  [EmailAddress]
  public string Email { get; set; }

  [Required]
  [MinLength(6)]
  public string Password { get; set; }

  [Required]
  [Compare("Password")]
  public string ConfirmPassword { get; set; }
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Response<string>>
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly IMapper _mapper;
  public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
  {
    _userManager = userManager;
    _mapper = mapper;
  }

  public async Task<Response<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
  {
    var user = new ApplicationUser
    {
      Email = request.Email,
      UserName = request.Email
    };

    var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
    if (userWithSameEmail != null)
    {
      var exception = new ApiException($"Email {request.Email} is already registered.");
      exception.Data["DataMessage"] = userWithSameEmail.Id;
      throw exception;
    }

    var result = await _userManager.CreateAsync(user, request.Password);
    if (result.Succeeded)
    {
      await _userManager.AddToRoleAsync(user, Roles.BasicUser.ToString());

      return new Response<string>(user.Id, message: "User Registered.");
    }
    else
    {
      var errors = new List<string>();
      foreach (var error in result.Errors)
      {
        errors.Add(error.Description);
      }

      return new Response<string> { Errors = errors, Succeeded = false, Message = "Register failed due to errors." };
    }
  }
}
