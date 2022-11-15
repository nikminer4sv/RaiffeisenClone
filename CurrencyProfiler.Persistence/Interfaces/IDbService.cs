namespace CurrencyProfiler.Persistence.Interfaces;

public interface IDbService<T>
{
    Task AddAsync(T entity);
}