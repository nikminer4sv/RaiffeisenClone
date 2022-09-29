using Microsoft.EntityFrameworkCore;
using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.Interfaces;

namespace RaiffeisenClone.Persistence.Repositories;

public class DepositRepository : IRepository<Deposit>
{
    private readonly ApplicationDbContext _context;

    public DepositRepository(ApplicationDbContext context) => (_context) = (context);
    
    public async Task<IEnumerable<Deposit>> GetAllAsync()
    {
        return await _context.Deposits.ToListAsync();
    }

    public async Task<Deposit?> GetByIdAsync(Guid id)
    {
        return await _context.Deposits.FindAsync(id);
    }

    public async Task AddAsync(Deposit obj)
    {
        await _context.Deposits.AddAsync(obj);
    }

    public async Task DeleteAsync(Guid Id)
    {
        Deposit? deposit = await _context.Deposits.FindAsync(Id);
        if (deposit is not null)
        {
            _context.Deposits.Remove(deposit);
        }
    }

    public async Task UpdateAsync(Deposit obj)
    {
        _context.Deposits.Update(obj);
    }

    public async Task<bool> ContainsAsync(Deposit obj)
    {
        return await _context.Deposits.ContainsAsync(obj);
    }
    
    public async Task<bool> ContainsAsync(Guid id)
    {
        return await _context.Deposits.AnyAsync(d => d.Id == id);
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