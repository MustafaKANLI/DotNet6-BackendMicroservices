namespace UsersService.Infrastructure.Persistence.Repositories;

using UsersService.Application.Interfaces.Repositories;
using UsersService.Domain.Entities;
using UsersService.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

public class CustomerRepositoryAsync: GenericRepositoryAsync<Customer>, ICustomerRepositoryAsync
{
  private readonly DbSet<Customer> _Customers;

  public CustomerRepositoryAsync(UsersServiceDbContext dbContext) : base(dbContext)
  {
     _Customers = dbContext.Customers;
  }
}
