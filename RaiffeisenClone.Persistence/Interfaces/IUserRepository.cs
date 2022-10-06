using RaiffeisenClone.Domain;

namespace RaiffeisenClone.Persistence.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetUserByRefreshToken(string token);
}