using Microsoft.AspNetCore.Mvc;
using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.Repositories;

namespace RaiffeisenClone.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
public class DepositController
{
    protected readonly DepositRepository _depositRepository;

    public DepositController(UserRepository userRepository, DepositRepository depositRepository) => _depositRepository = depositRepository;
    
    [HttpGet]
    public async Task<IEnumerable<Deposit>> GetAll()
    {
        return await _depositRepository.GetAllAsync();
    }

    [HttpGet]
    public async Task<Deposit> GetById(Guid id)
    {
        return await _depositRepository.GetByIdAsync(id) ?? new Deposit();
    }

    [HttpPost]
    public async Task Add(Deposit deposit)
    {
        deposit.Id = Guid.NewGuid();
        await _depositRepository.AddAsync(deposit);
        await _depositRepository.SaveAsync();
    }

    [HttpPut]
    public async Task Update(Deposit deposit)
    {
        await _depositRepository.UpdateAsync(deposit);
        await _depositRepository.SaveAsync();
    }

    [HttpDelete]
    public async Task Delete(Guid id)
    {
        await _depositRepository.DeleteAsync(id);
        await _depositRepository.SaveAsync();
    }
    
}