using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.Interfaces;
using RaiffeisenClone.Persistence.Repositories;

namespace RaiffeisenClone.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IDepositRepository Deposits { get; private set; }
    public IAvatarRepository Avatars { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Deposits = new DepositRepository(_context);
        Avatars = new AvatarRepository(_context);
    }
    
    public async void Dispose()
    {
        await _context.DisposeAsync();
    }
    
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}