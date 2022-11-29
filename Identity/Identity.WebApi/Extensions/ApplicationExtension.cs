using FluentValidation.AspNetCore;
using Identity.Application.Interfaces;
using Identity.Application.MappingProfiles;
using Identity.Application.Services;
using Identity.Application.ViewModels.RegisterViewModel;

namespace Identity.WebApi.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddAutoMapper(typeof(UserProfile));
        collection.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterViewModelValidator>());
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<IJwtService, JwtService>();
        collection.AddScoped<IAuthService, AuthService>();
        return collection;
    }
}