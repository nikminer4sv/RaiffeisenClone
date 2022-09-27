using Microsoft.AspNetCore.Mvc;
using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.Repositories;

namespace RaiffeisenClone.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
public class UserController
{
    private readonly UserRepository _userRepository;

    public UserController(UserRepository userRepository) => _userRepository = userRepository;
    
    [HttpGet]
    public async Task<IEnumerable<User>> GetAll()
    {
        return await _userRepository.GetAllAsync();
    }

    [HttpGet]
    public async Task<User> GetById(Guid id)
    {
        return await _userRepository.GetByIdAsync(id) ?? new User();
    }

    [HttpPost]
    public async Task Add(User user)
    {
        user.Id = Guid.NewGuid();
        await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();
    }

    [HttpPut]
    public async Task Update(User user)
    {
        await _userRepository.UpdateAsync(user);
        await _userRepository.SaveAsync();
    }

    [HttpDelete]
    public async Task Delete(Guid id)
    {
        await _userRepository.DeleteAsync(id);
        await _userRepository.SaveAsync();
    }
}