namespace AuthService.Application.Features.SharedViewModels;

public class ApplicationUserViewModel
{
    public string Id { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
}

