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
    
    public async Task<UserViewModel> GetByIdAsync(Guid id)
    {
        User? user = await _userRepository.GetByIdAsync(id);
        UserViewModel userViewModel = _mapper.Map<UserViewModel>(user);
        return userViewModel;
    }
    
    public async Task<Guid> AddAsync(UserViewModel userViewModel)
    {
        User user = _mapper.Map<User>(userViewModel);
        user.Id = Guid.NewGuid();
        await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();
        return user.Id;
    }
    
    public async Task UpdateAsync(User user)
    {
        await _userRepository.UpdateAsync(user);
        await _userRepository.SaveAsync();
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await _userRepository.DeleteAsync(id);
        await _userRepository.SaveAsync();
    }
}