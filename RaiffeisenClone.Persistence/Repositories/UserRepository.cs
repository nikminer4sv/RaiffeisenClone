using Microsoft.EntityFrameworkCore;
using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.Interfaces;

namespace RaiffeisenClone.Persistence.Repositories;

public class UserRepository : IUserRepository
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

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetUserByRefreshToken(string token)
    {
        User? user = await _context.Users.Include(u => u.RefreshTokens).SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
        return user;
    }

    public async Task<Guid> AddAsync(User obj)
    {
        obj.Id = Guid.NewGuid();
        await _context.Users.AddAsync(obj);
        return obj.Id;
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
        _context.Users.Update(obj);
    }

    public async Task<bool> ContainsAsync(User obj)
    {
        return await _context.Users.ContainsAsync(obj);
    }
    
    public async Task<bool> ContainsAsync(Guid id)
    {
        return await _context.Users.AnyAsync(u => u.Id == id);
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