namespace RaiffeisenClone.Persistence.Interfaces;

public interface IUnitOfWork: IDisposable {
    IUserRepository Users {
        get;
    }
    IDepositRepository Deposits {
        get;
    }

    IAvatarRepository Avatars
    {
        get;
    }
    Task<int> SaveAsync();
}