using Identity.Domain;

namespace Identity.Persistence.Interfaces;

public interface IGenericRepository<T> where T: BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(T entity);
    Task DeleteAsync(T entity);
    Task UpdateAsync(T entity);
    Task<bool> ContainsAsync(Guid id);
    Task<bool> ContainsAsync(T entity);

    Task SaveAsync();
}