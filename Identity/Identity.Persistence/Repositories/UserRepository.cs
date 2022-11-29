using Identity.Domain;
using Identity.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence.Repositories;

public class UserRepository: GenericRepository <User> , IUserRepository {
    public UserRepository(ApplicationDbContext context): base(context) {}
    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetUserByRefreshToken(string token)
    {
        User? user = await _context.Users.Include(u => u.RefreshTokens).SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
        return user;
    }
}