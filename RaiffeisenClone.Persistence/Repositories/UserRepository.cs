using Microsoft.EntityFrameworkCore;
using RaiffeisenClone.Application.ViewModels;
using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.Interfaces;

namespace RaiffeisenClone.Persistence.Repositories;

public class UserRepository : IRepository<User>
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) => (_context) = (context);
    
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task AddAsync(User obj)
    {
        await _context.Users.AddAsync(obj);
    }

    public async Task DeleteAsync(Guid Id)
    {
        User? user = await _context.Users.FindAsync(Id);
        if (user is not null)
        {
            _context.Users.Remove(user);
        }
    }

    public async Task UpdateAsync(User obj)
    {
        _context.Entry(obj).State = EntityState.Modified;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}