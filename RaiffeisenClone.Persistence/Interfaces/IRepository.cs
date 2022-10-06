namespace RaiffeisenClone.Persistence.Interfaces;

public interface IRepository<T> : IDisposable
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(T obj);
    Task DeleteAsync(Guid Id);
    Task UpdateAsync(T obj);
    Task<bool> ContainsAsync(Guid id);
    Task<bool> ContainsAsync(T obj);
    Task SaveAsync();
}