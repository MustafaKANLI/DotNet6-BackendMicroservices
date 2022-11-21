namespace UsersService.Infrastructure.Persistence.Repositories;

using UsersService.Application.Interfaces.Repositories;
using UsersService.Domain.Entities;
using UsersService.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

public class UserRepositoryAsync: GenericRepositoryAsync<User>, IUserRepositoryAsync
{
  private readonly DbSet<User> _Users;

  public UserRepositoryAsync(UsersServiceDbContext dbContext) : base(dbContext)
  {
    _Users = dbContext.Users;
  }
}
