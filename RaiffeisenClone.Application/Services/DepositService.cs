using AutoMapper;
using RaiffeisenClone.Application.Interfaces;
using RaiffeisenClone.Application.ViewModels;
using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.Interfaces;
using RaiffeisenClone.Persistence.Repositories;

namespace RaiffeisenClone.Application.Services;

public class DepositService : IDepositService
{
    private readonly IDepositRepository _depositRepository;
    private readonly IMapper _mapper;

    public DepositService(IDepositRepository depositRepository, IMapper mapper) => 
        (_depositRepository, _mapper) = (depositRepository, mapper);

    public async Task<IEnumerable<DepositViewModel>> GetAllAsync(Guid userId)
    {
        return (await _depositRepository.GetAllAsync()).Where(d => d.UserId == userId).Select(d => _mapper.Map<DepositViewModel>(d));
    }
    
    public async Task<DepositViewModel> GetByIdAsync(Guid id, Guid userId)
    {
        Deposit? deposit = await _depositRepository.GetByIdAsync(id);
        if (deposit is null || deposit.UserId != userId)
            throw new KeyNotFoundException("Deposit not found.");
        DepositViewModel depositViewModel = _mapper.Map<DepositViewModel>(deposit);
        return depositViewModel;
    }
    
    public async Task<Guid> AddAsync(DepositViewModel depositViewModel, Guid userId)
    {
        Deposit deposit = _mapper.Map<Deposit>(depositViewModel);
        deposit.UserId = userId;
        await _depositRepository.AddAsync(deposit);
        await _depositRepository.SaveAsync();
        return deposit.Id;
    }
    
    public async Task<DepositUpdateViewModel> UpdateAsync(DepositUpdateViewModel depositUpdateViewModel, Guid userId)
    {
        Deposit? temp = await _depositRepository.GetByIdAsync(depositUpdateViewModel.Id);
        if (temp?.UserId != userId)
            throw new KeyNotFoundException("Deposit not found.");
        
        temp = _mapper.Map<Deposit>(depositUpdateViewModel);
        temp.UserId = userId;
        await _depositRepository.UpdateAsync(temp);
        await _depositRepository.SaveAsync();
        return depositUpdateViewModel;
    }
    
    public async Task DeleteAsync(Guid id, Guid userId)
    {
        Deposit? temp = await _depositRepository.GetByIdAsync(id);
        if (temp is null || temp.UserId != userId)
            throw new KeyNotFoundException("Deposit not found.");
        await _depositRepository.DeleteAsync(id);
        await _depositRepository.SaveAsync();
    }
}