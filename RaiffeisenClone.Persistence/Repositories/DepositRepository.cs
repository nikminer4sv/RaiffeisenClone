using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.Interfaces;

namespace RaiffeisenClone.Persistence.Repositories;

class DepositRepository: GenericRepository <Deposit> , IDepositRepository {
    public DepositRepository(ApplicationDbContext context): base(context) {}
}