using RaiffeisenClone.Persistence;
using RaiffeisenClone.Persistence.Interfaces;

namespace RaiffeisenClone.Tests.Common;

public static class UnitOfWorkFactory
{
    public static IUnitOfWork Create(ApplicationDbContext context)
    {
        IUnitOfWork unitOfWork = new UnitOfWork(context);
        return unitOfWork;
    }

    public static void Delete(IUnitOfWork unitOfWork)
    {
        unitOfWork.Dispose();
    }
}