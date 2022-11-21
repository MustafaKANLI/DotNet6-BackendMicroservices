namespace AuthService.Infrastructure.Persistence.Seeds;

using AuthService.Domain.Entities;
using AuthService.Domain.Enums;
using Microsoft.AspNetCore.Identity;

public static class DefaultRoles
{
  public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
  {
    //Seed Roles
    await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
    await roleManager.CreateAsync(new IdentityRole(Roles.Trainee.ToString()));
    await roleManager.CreateAsync(new IdentityRole(Roles.Trainer.ToString()));
    await roleManager.CreateAsync(new IdentityRole(Roles.BasicUser.ToString()));
  }
}
