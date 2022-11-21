namespace OtherService.Infrastructure.Persistence;

using OtherService.Application.Interfaces.Repositories;
using OtherService.Infrastructure.Persistence.Contexts;
using OtherService.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistration
{
  public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    if (bool.Parse(configuration.GetSection("UseInMemoryDatabase").Value))
    {
      services.AddDbContext<OtherServiceDbContext>(options =>
          options.UseInMemoryDatabase("OtherServiceDb"));
    }
    else
    {

      services.AddDbContext<OtherServiceDbContext>(options =>
      options.UseNpgsql(
         configuration.GetConnectionString("DefaultConnection"),
         b => b.MigrationsAssembly(typeof(OtherServiceDbContext).Assembly.FullName)));
    }

    services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
    services.AddScoped<IEntityRepositoryAsync, EntityRepositoryAsync>();
  }
}
