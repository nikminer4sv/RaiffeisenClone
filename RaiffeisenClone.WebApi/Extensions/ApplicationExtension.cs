using FluentValidation.AspNetCore;
using RaiffeisenClone.Application.Interfaces;
using RaiffeisenClone.Application.MappingProfiles;
using RaiffeisenClone.Application.Services;
using RaiffeisenClone.Application.Validators;

namespace RaiffeisenClone.WebApi.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddAutoMapper(typeof(UserProfile), typeof(DepositProfile));
        collection.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterViewModelValidator>());
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<IDepositService, DepositService>();
        collection.AddScoped<IJwtService, JwtService>();
        collection.AddScoped<IAuthService, AuthService>();
        collection.AddSingleton<IEmailSender, EmailSender>();
        collection.AddScoped<IAvatarService, FirebaseAvatarService>();
        return collection;
    }
}