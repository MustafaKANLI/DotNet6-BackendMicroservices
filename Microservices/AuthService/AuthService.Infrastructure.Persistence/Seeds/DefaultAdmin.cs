namespace AuthService.Infrastructure.Persistence.Seeds;

using AuthService.Domain.Enums;
using AuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;


public static class DefaultAdmin
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        //Seed Default User
        var defaultUser = new ApplicationUser
        {
            UserName = "admin@f.com",
            Email = "admin@f.com",
        };
        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "123Asd.");
                await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                await userManager.AddToRoleAsync(defaultUser, Roles.BasicUser.ToString());
                await userManager.AddToRoleAsync(defaultUser, Roles.Trainee.ToString());
                await userManager.AddToRoleAsync(defaultUser, Roles.Trainer.ToString());
            }
        }
    }
}
