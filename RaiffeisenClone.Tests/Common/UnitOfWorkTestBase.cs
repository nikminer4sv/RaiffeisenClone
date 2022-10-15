using System;
using RaiffeisenClone.Persistence;
using RaiffeisenClone.Persistence.Interfaces;

namespace RaiffeisenClone.Tests.Common;

public class UnitOfOWorkTestBase : IDisposable
{
    private readonly ApplicationDbContext _context;
    
    protected readonly IUnitOfWork UnitOfWork;

    protected UnitOfOWorkTestBase()
    {
        _context = ContextFactory.Create();
        UnitOfWork = UnitOfWorkFactory.Create(_context);
    }

    public void Dispose()
    {
        ContextFactory.DeleteData(_context);
        UnitOfWorkFactory.Delete(UnitOfWork);
    }
}