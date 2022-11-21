namespace AuthService.Infrastructure.Persistence;

using AuthService.Infrastructure.Persistence.Contexts;
using AuthService.Application.Interfaces.Repositories;
using AuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthService.Infrastructure.Persistence.Repositories;

public static class ServiceRegistration
{
  public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    if (bool.Parse(configuration.GetSection("UseInMemoryDatabase").Value))
    {
      services.AddDbContext<AuthServiceDbContext>(options =>
          options.UseInMemoryDatabase("AuthServiceDb"));
    }
    else
    {

      services.AddDbContext<AuthServiceDbContext>(options =>
      options.UseNpgsql(
         configuration.GetConnectionString("DefaultConnection"),
         b => b.MigrationsAssembly(typeof(AuthServiceDbContext).Assembly.FullName)));
    }

    services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));

    services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AuthServiceDbContext>().AddDefaultTokenProviders();

    services.Configure<IdentityOptions>(options =>
    {
      options.User.AllowedUserNameCharacters = "abcçdefgğhıijklmnoöprsştuüvyzxqwABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZXQW-._@+1234567890";
    });
  }
}
