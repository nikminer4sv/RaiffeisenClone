using Microsoft.EntityFrameworkCore;
using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.Interfaces;

namespace RaiffeisenClone.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context) => (_context) = (context);


    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        var deposit = await _context.Set<T>().FindAsync(id);
        if (deposit is not null)
            _context.Entry(deposit).State = EntityState.Detached;
        return deposit;
    }

    public async Task<Guid> AddAsync(T entity)
    {
        entity.Id = Guid.NewGuid();
        await _context.Set<T>().AddAsync(entity);
        return entity.Id;
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public async Task<bool> ContainsAsync(Guid id)
    {
        return await _context.Set<T>().ContainsAsync(await GetByIdAsync(id));
    }

    public async Task<bool> ContainsAsync(T entity)
    {
        return await _context.Set<T>().ContainsAsync(entity);
    }
}