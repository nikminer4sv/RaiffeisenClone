using RaiffeisenClone.Domain;

namespace RaiffeisenClone.Persistence.Interfaces;

public interface IUserRepository : IRepository<User>
{
    public Task<User?> GetByUsernameAsync(string username);

    public Task<User?> GetUserByRefreshToken(string token);
}