namespace RaiffeisenClone.Persistence.Interfaces;

public interface IRepository<T> : IDisposable
{
    Task<IEnumerable<T>> GetStudents();
    Task<T>? GetByIdAsync(Guid id);
    Task AddAsync(T obj);
    Task DeleteAsync(Guid Id);
    Task UpdateAsync(T obj);
    Task SaveAsync();
}