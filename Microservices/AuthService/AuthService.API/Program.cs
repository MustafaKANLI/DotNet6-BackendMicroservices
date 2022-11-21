using AuthService.Application;
using Common.Middlewares;
using Microsoft.AspNetCore.Http.Features;
using AuthService.API.Extensions;
using AuthService.Infrastructure.Persistence;
using AuthService.Application.Interfaces.Repositories;
using AuthService.Infrastructure.Persistence.Seeds;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using AuthService.Domain.Entities;
using AuthService.Application.Settings;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json")
  .Build();

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
  options.InvalidModelStateResponseFactory = actionContext =>
  {
    return Common.Wrappers.Response<string>.ModelValidationErrorResponse(actionContext);
  };
});

builder.Services.AddApplicationLayer(config);
builder.Services.AddPersistenceInfrastructure(config);
builder.Services.AddSwaggerExtension();

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(
    builder =>
    {
      builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.Configure<FormOptions>(o =>
{
  o.ValueLengthLimit = int.MaxValue;
  o.MultipartBodyLengthLimit = int.MaxValue;
  o.MemoryBufferThreshold = int.MaxValue;
});

builder.Services.Configure<JWTSettings>(config.GetSection("JWTSettings"));

builder.Services.AddHealthChecks();

builder.Services.AddMassTransit(o =>
{
  //o.AddConsumer<GetEntityDataConsumer>();

  o.UsingRabbitMq((context, cfg) =>
  {
    cfg.Host("rabbitmq", "/", h =>
    {
      h.Username("admin");
      h.Password("admin");
    });

    cfg.UseNewtonsoftJsonSerializer();
    cfg.ConfigureEndpoints(context);
  });
});

builder.Services.AddOptions<MassTransitHostOptions>().Configure(options =>
{
  options.WaitUntilStarted = true;
  options.StartTimeout = TimeSpan.FromSeconds(10);
  options.StopTimeout = TimeSpan.FromSeconds(30);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;

  try
  {
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await DefaultRoles.SeedAsync(userManager, roleManager);
    await DefaultTrainer.SeedAsync(userManager, roleManager);
    await DefaultAdmin.SeedAsync(userManager, roleManager);

  }
  catch (Exception ex)
  {
    Console.Error.WriteLine(ex);
  }
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseCors();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

