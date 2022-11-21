namespace UsersService.Application.Interfaces.Repositories;

public interface IGenericRepositoryAsync<T> where T : class
{
  Task<T> GetByIdAsync(int id);
  Task<IReadOnlyList<T>> GetAllAsync();
  Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
  Task<T> AddAsync(T entity);
  Task<int> GetDataCount();
  Task UpdateAsync(T entity);
  Task DeleteAsync(T entity);
  Task MarkUnchangedAsync(T entity);
  Task MarkDetachedAsync(T entity);
  Task MarkModifiedAsync(T entity);
  Task ClearChangeTracker();
}
