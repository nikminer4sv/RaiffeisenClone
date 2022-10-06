using RaiffeisenClone.Application.ViewModels;

namespace RaiffeisenClone.Application.Interfaces;

public interface IDepositService : IService
{
    Task<IEnumerable<DepositViewModel>> GetAllAsync(Guid userId);

    Task<DepositViewModel> GetByIdAsync(Guid id, Guid userId);

    Task<Guid> AddAsync(DepositViewModel depositViewModel, Guid userId);

    Task<DepositUpdateViewModel> UpdateAsync(DepositUpdateViewModel depositUpdateViewModel, Guid userId);

    Task DeleteAsync(Guid id, Guid userId);
}