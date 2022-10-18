using AutoMapper;
using RaiffeisenClone.Application.Interfaces;
using RaiffeisenClone.Application.ViewModels;
using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.Interfaces;

namespace RaiffeisenClone.Application.Services;

public class DepositService : IDepositService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;

    public DepositService(IUnitOfWork unitOfWork, IMapper mapper, IEmailSender emailSender) => 
        (_unitOfWork, _mapper, _emailSender) = (unitOfWork, mapper, emailSender);

    public async Task<IEnumerable<DepositViewModel>> GetAllAsync(Guid userId)
    {
        return (await _unitOfWork.Deposits.GetAllAsync()).Where(d => d.UserId == userId).Select(d => _mapper.Map<DepositViewModel>(d));
    }
    
    public async Task<DepositViewModel> GetByIdAsync(Guid id, Guid userId)
    {
        Deposit? deposit = await _unitOfWork.Deposits.GetByIdAsync(id);
        if (deposit is null || deposit.UserId != userId)
            throw new KeyNotFoundException("Deposit not found.");
        DepositViewModel depositViewModel = _mapper.Map<DepositViewModel>(deposit);
        return depositViewModel;
    }
    
    public async Task<Guid> AddAsync(DepositViewModel depositViewModel, Guid userId)
    {
        Deposit deposit = _mapper.Map<Deposit>(depositViewModel);
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        deposit.UserId = userId;
        await _unitOfWork.Deposits.AddAsync(deposit);
        await _unitOfWork.SaveAsync();
        _emailSender.Send(user!.Email);
        return deposit.Id;
    }
    
    public async Task<DepositUpdateViewModel> UpdateAsync(DepositUpdateViewModel depositUpdateViewModel, Guid userId)
    {
        var temp = await _unitOfWork.Deposits.GetByIdAsync(depositUpdateViewModel.Id);
        if (temp?.UserId != userId)
            throw new KeyNotFoundException("Deposit not found.");
        
        temp = _mapper.Map<Deposit>(depositUpdateViewModel);
        temp.UserId = userId;
        await _unitOfWork.Deposits.UpdateAsync(temp);
        await _unitOfWork.SaveAsync();
        return depositUpdateViewModel;
    }
    
    public async Task DeleteAsync(Guid id, Guid userId)
    {
        var temp = await _unitOfWork.Deposits.GetByIdAsync(id);
        if (temp is null || temp.UserId != userId)
            throw new KeyNotFoundException("Deposit not found.");
        await _unitOfWork.Deposits.DeleteAsync(temp);
        await _unitOfWork.SaveAsync();
    }
}