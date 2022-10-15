using AutoMapper;
using RaiffeisenClone.Application.ViewModels;
using RaiffeisenClone.Application.Helpers;
using RaiffeisenClone.Application.Interfaces;
using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.Interfaces;

namespace RaiffeisenClone.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public UserService(IUnitOfWork unitOfWork, IMapper mapper) => 
        (_unitOfWork, _mapper) = (unitOfWork, mapper);

    public async Task<IEnumerable<UserViewModel>> GetAllAsync()
    {
        return (await _unitOfWork.Users.GetAllAsync()).Select(u => _mapper.Map<UserViewModel>(u));
    }
    
    public async Task<User> GetByIdAsync(Guid id)
    {
        User? user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user is null)
            throw new KeyNotFoundException("User not found.");
        return user;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        User? user = await _unitOfWork.Users.GetByUsernameAsync(username);
        if (username is null)
            throw new KeyNotFoundException("User not found.");
        return user;
    }

    public async Task<User?> GetByRefreshToken(string token)
    {
        User? user = await _unitOfWork.Users.GetUserByRefreshToken(token);
        if (user is null)
            throw new KeyNotFoundException("User not found.");
        return user;
    }
    
    public async Task<Guid> AddAsync(RegisterViewModel registerViewModel)
    {
        if (await IsUserExist(registerViewModel.Username))
            throw new Exception("Username is already taken");
        
        User user = _mapper.Map<User>(registerViewModel);
        user.PasswordHash = HashHelper.HashPassword(registerViewModel.Password);
        var id = await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveAsync();
        return id;
    }
    
    public async Task UpdateAsync(User user)
    {
        bool exist = await _unitOfWork.Users.ContainsAsync(user.Id);
        if (!exist)
            throw new KeyNotFoundException("User not found.");
        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.SaveAsync();
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user is null)
            throw new KeyNotFoundException("User not found.");
        await _unitOfWork.Users.DeleteAsync(user);
        await _unitOfWork.SaveAsync();
    }

    public async Task<bool> IsUserExist(string username)
    {
        User? user = await _unitOfWork.Users.GetByUsernameAsync(username);
        if (user is null)
            return false;
        return true;
    }
}