namespace RaiffeisenClone.Persistence.Interfaces;

public interface IUnitOfWork: IDisposable {
    IUserRepository Users {
        get;
    }
    IDepositRepository Deposits {
        get;
    }
    Task<int> SaveAsync();
}