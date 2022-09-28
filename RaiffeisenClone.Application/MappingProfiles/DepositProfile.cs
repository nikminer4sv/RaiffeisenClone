using AutoMapper;
using RaiffeisenClone.Application.ViewModels;
using RaiffeisenClone.Domain;

namespace RaiffeisenClone.Application.MappingProfiles;

public class DepositProfile : Profile
{
    public DepositProfile()
    {
        CreateMap<Deposit, DepositViewModel>().ReverseMap();
    }
}