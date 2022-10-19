namespace EmailWorker.Interfaces;

public interface IDbService<T>
{
    Task Add(T entity);
}