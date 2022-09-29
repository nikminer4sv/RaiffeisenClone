using AutoMapper;
using RaiffeisenClone.Application.ViewModels;
using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.Repositories;

namespace RaiffeisenClone.Application.Services;

public class DepositService
{
    private readonly DepositRepository _depositRepository;
    private readonly IMapper _mapper;
    
    public DepositService(DepositRepository depositRepository, IMapper mapper) => 
        (_depositRepository, _mapper) = (depositRepository, mapper);

    public async Task<IEnumerable<DepositViewModel>> GetAllAsync()
    {
        return (await _depositRepository.GetAllAsync()).Select(d => _mapper.Map<DepositViewModel>(d));
    }
    
    public async Task<DepositViewModel> GetByIdAsync(Guid id)
    {
        Deposit? deposit = await _depositRepository.GetByIdAsync(id);
        if (deposit is null)
            throw new KeyNotFoundException("Deposit not found.");
        DepositViewModel depositViewModel = _mapper.Map<DepositViewModel>(deposit);
        return depositViewModel;
    }
    
    public async Task<Guid> AddAsync(DepositViewModel depositViewModel)
    {
        Deposit deposit = _mapper.Map<Deposit>(depositViewModel);
        deposit.Id = Guid.NewGuid();
        await _depositRepository.AddAsync(deposit);
        await _depositRepository.SaveAsync();
        return deposit.Id;
    }
    
    public async Task UpdateAsync(Deposit deposit)
    {
        bool exist = await _depositRepository.ContainsAsync(deposit.Id);
        if (!exist)
            throw new KeyNotFoundException("Deposit not found.");
        await _depositRepository.UpdateAsync(deposit);
        await _depositRepository.SaveAsync();
    }
    
    public async Task DeleteAsync(Guid id)
    {
        bool exist = await _depositRepository.ContainsAsync(id);
        if (!exist)
            throw new KeyNotFoundException("Deposit not found.");
        await _depositRepository.DeleteAsync(id);
        await _depositRepository.SaveAsync();
    }
}