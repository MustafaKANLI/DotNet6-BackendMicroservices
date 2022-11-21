namespace AuthService.Application.Features.SharedViewModels;

public class AuthenticationResponse
{
  public string Id { get; set; }
  public string Email { get; set; }
  public List<string> Roles { get; set; }
  public string JWToken { get; set; }
  public int? Expires { get; set; }
}

