namespace RaiffeisenClone.Persistence.Interfaces;

public interface IUnitOfWork: IDisposable {
    IDepositRepository Deposits {
        get;
    }

    IAvatarRepository Avatars
    {
        get;
    }
    Task<int> SaveAsync();
}