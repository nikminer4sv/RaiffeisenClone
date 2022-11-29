using Identity.Domain;

namespace Identity.Persistence.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetUserByRefreshToken(string token);
}