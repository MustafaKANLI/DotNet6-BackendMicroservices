namespace OtherService.Infrastructure.Persistence.Repositories;

using OtherService.Application.Interfaces.Repositories;
using OtherService.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
{
  private readonly OtherServiceDbContext _dbContext;

  public GenericRepositoryAsync(OtherServiceDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public virtual async Task<T> GetByIdAsync(int id)
  {
    return await _dbContext.Set<T>().FindAsync(id);
  }

  public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
  {
    return await _dbContext
        .Set<T>()
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .AsNoTracking()
        .ToListAsync();
  }

  public async Task<T> AddAsync(T entity)
  {
    await _dbContext.Set<T>().AddAsync(entity);
    await _dbContext.SaveChangesAsync();
    return entity;
  }

  public async Task UpdateAsync(T entity)
  {
    _dbContext.Entry(entity).State = EntityState.Modified;
    //_dbContext.Update(entity);
    await _dbContext.SaveChangesAsync();
  }

  public async Task DeleteAsync(T entity)
  {
    _dbContext.Set<T>().Remove(entity);
    await _dbContext.SaveChangesAsync();
  }

  public async Task<IReadOnlyList<T>> GetAllAsync()
  {
    return await _dbContext
         .Set<T>()
         .ToListAsync();
  }

  public async Task<int> GetDataCount()
  {
    return await _dbContext
      .Set<T>()
      .CountAsync();
  }

  public async Task MarkUnchangedAsync(T entity)
  {
    _dbContext.Entry(entity).State = EntityState.Unchanged;
  }

  public async Task MarkDetachedAsync(T entity)
  {
    _dbContext.Entry(entity).State = EntityState.Detached;
  }

  public async Task MarkModifiedAsync(T entity)
  {
    _dbContext.Entry(entity).State = EntityState.Modified;
  }

  public async Task ClearChangeTracker()
  {
    _dbContext.ChangeTracker.Clear();
  }
}
