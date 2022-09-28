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
        await _depositRepository.UpdateAsync(deposit);
        await _depositRepository.SaveAsync();
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await _depositRepository.DeleteAsync(id);
        await _depositRepository.SaveAsync();
    }
}