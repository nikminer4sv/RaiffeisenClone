using AutoMapper;
using Identity.Application.ViewModels;
using Identity.Application.ViewModels.RegisterViewModel;
using Identity.Domain;

namespace Identity.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserViewModel>().ReverseMap();
        CreateMap<RegisterViewModel, User>();
    }
}