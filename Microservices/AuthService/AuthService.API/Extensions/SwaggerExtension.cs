namespace AuthService.API.Extensions;

using Microsoft.OpenApi.Models;

public static class SwaggerExtension
{
  public static void AddSwaggerExtension(this IServiceCollection services)
  {
    services.AddSwaggerGen(c =>
    {
      c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
      c.IgnoreObsoleteActions();
      c.IgnoreObsoleteProperties();
      c.CustomSchemaIds(type => type.FullName);
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "Other Service", Version = "v1" });

      c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
      });

      c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
          {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
                Scheme = "Bearer",
                Name = "Bearer",
                In = ParameterLocation.Header,
            }, new List<string>()
          },
        });
    });
  }
}
