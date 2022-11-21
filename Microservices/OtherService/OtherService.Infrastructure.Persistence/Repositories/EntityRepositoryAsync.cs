namespace OtherService.Infrastructure.Persistence.Repositories;

using OtherService.Application.Interfaces.Repositories;
using OtherService.Domain.Entities;
using OtherService.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

public class EntityRepositoryAsync : GenericRepositoryAsync<Entity>, IEntityRepositoryAsync
{
  private readonly DbSet<Entity> _Entities;

  public EntityRepositoryAsync(OtherServiceDbContext dbContext) : base(dbContext)
  {
    _Entities = dbContext.Entities;
  }
}
