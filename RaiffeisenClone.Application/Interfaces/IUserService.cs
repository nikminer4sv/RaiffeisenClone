using RaiffeisenClone.Application.ViewModels;
using RaiffeisenClone.Domain;

namespace RaiffeisenClone.Application.Interfaces;

public interface IUserService : IService
{ 
    Task<IEnumerable<UserViewModel>> GetAllAsync();

    Task<User> GetByIdAsync(Guid id);

    Task<User?> GetByUsernameAsync(string username);

    Task<User?> GetByRefreshToken(string token);

    Task<Guid> AddAsync(RegisterViewModel registerViewModel);

    Task UpdateAsync(User user);

    Task DeleteAsync(Guid id);

    Task<bool> IsUserExist(string username);
}