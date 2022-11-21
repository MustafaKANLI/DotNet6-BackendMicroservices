namespace AuthService.Infrastructure.Persistence.Contexts;

using AuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AuthServiceDbContext : IdentityDbContext<ApplicationUser>
{
  public AuthServiceDbContext(DbContextOptions<AuthServiceDbContext> options) : base(options)
  {
  }
  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
    builder.HasDefaultSchema("Identity");
    builder.Entity<ApplicationUser>(entity =>
    {
      entity.ToTable(name: "User");
    });

    builder.Entity<IdentityRole>(entity =>
    {
      entity.ToTable(name: "Role");
    });
    builder.Entity<IdentityUserRole<string>>(entity =>
    {
      entity.ToTable("UserRoles");
    });

    builder.Entity<IdentityUserClaim<string>>(entity =>
    {
      entity.ToTable("UserClaims");
    });

    builder.Entity<IdentityUserLogin<string>>(entity =>
    {
      entity.ToTable("UserLogins");
    });

    builder.Entity<IdentityRoleClaim<string>>(entity =>
    {
      entity.ToTable("RoleClaims");

    });

    builder.Entity<IdentityUserToken<string>>(entity =>
    {
      entity.ToTable("UserTokens");
    });
  }
}

