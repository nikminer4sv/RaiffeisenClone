using AutoMapper;
using RaiffeisenClone.Application.ViewModels;
using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.Repositories;

namespace RaiffeisenClone.Application.Services;

public class UserService
{
    private readonly UserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UserService(UserRepository userRepository, IMapper mapper) => 
        (_userRepository, _mapper) = (userRepository, mapper);

    public async Task<IEnumerable<UserViewModel>> GetAllAsync()
    {
        return (await _userRepository.GetAllAsync()).Select(u => _mapper.Map<UserViewModel>(u));
    }
    
    public async Task<User> GetByIdAsync(Guid id)
    {
        User? user = await _userRepository.GetByIdAsync(id);
        if (user is null)
            throw new KeyNotFoundException("User not found.");
        return user;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        User? user = await _userRepository.GetByUsernameAsync(username);
        if (username is null)
            throw new KeyNotFoundException("User not found.");
        return user;
    }

    public async Task<User?> GetByRefreshToken(string token)
    {
        User? user = await _userRepository.GetUserByRefreshToken(token);
        if (user is null)
            throw new KeyNotFoundException("User not found.");
        return user;
    }
    
    public async Task<Guid> AddAsync(RegisterViewModel registerViewModel)
    {
        if (await IsUserExist(registerViewModel.Username))
            throw new Exception("Username is already taken");
        
        User user = _mapper.Map<User>(registerViewModel);
        user.Id = Guid.NewGuid();
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerViewModel.Password);
        await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();
        return user.Id;
    }
    
    public async Task UpdateAsync(User user)
    {
        bool exist = await _userRepository.ContainsAsync(user.Id);
        if (!exist)
            throw new KeyNotFoundException("User not found.");
        await _userRepository.UpdateAsync(user);
        await _userRepository.SaveAsync();
    }
    
    public async Task DeleteAsync(Guid id)
    {
        bool exist = await _userRepository.ContainsAsync(id);
        if (!exist)
            throw new KeyNotFoundException("User not found.");
        await _userRepository.DeleteAsync(id);
        await _userRepository.SaveAsync();
    }

    public async Task<bool> IsUserExist(string username)
    {
        User? user = await _userRepository.GetByUsernameAsync(username);
        if (user is null)
            return false;
        return true;
    }
}