using Microsoft.AspNetCore.Mvc;
using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.Repositories;

namespace RaiffeisenClone.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ApiController
{
    private readonly UserRepository _userRepository;
    private readonly DepositRepository _depositRepository;

    public ApiController(UserRepository userRepository, DepositRepository depositRepository) => 
        (_userRepository, _depositRepository) = (userRepository, depositRepository);

    [HttpGet]
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _userRepository.GetAllAsync();
    }

    [HttpGet]
    public async Task<User> GetUserById(Guid id)
    {
        return await _userRepository.GetByIdAsync(id) ?? new User();
    }

    [HttpPost]
    public async Task AddUser(User user)
    {
        user.Id = Guid.NewGuid();
        await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();
    }

    [HttpPut]
    public async Task UpdateUser(User user)
    {
        await _userRepository.UpdateAsync(user);
        await _userRepository.SaveAsync();
    }

    [HttpDelete]
    public async Task DeleteUser(Guid id)
    {
        await _userRepository.DeleteAsync(id);
        await _userRepository.SaveAsync();
    }
    
    // ---------------------------
    
    [HttpGet]
    public async Task<IEnumerable<Deposit>> GetAllDeposits()
    {
        return await _depositRepository.GetAllAsync();
    }

    [HttpGet]
    public async Task<Deposit> GetDepositById(Guid id)
    {
        return await _depositRepository.GetByIdAsync(id) ?? new Deposit();
    }

    [HttpPost]
    public async Task AddDeposit(Deposit deposit)
    {
        deposit.Id = Guid.NewGuid();
        await _depositRepository.AddAsync(deposit);
        await _depositRepository.SaveAsync();
    }

    [HttpPut]
    public async Task UpdateDeposit(Deposit deposit)
    {
        await _depositRepository.UpdateAsync(deposit);
        await _depositRepository.SaveAsync();
    }

    [HttpDelete]
    public async Task DeleteDeposit(Guid id)
    {
        await _depositRepository.DeleteAsync(id);
        await _depositRepository.SaveAsync();
    }
    
}