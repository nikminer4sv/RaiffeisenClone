using RaiffeisenClone.Application.MappingProfiles;
using RaiffeisenClone.Application.Services;

namespace RaiffeisenClone.WebApi.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddAutoMapper(typeof(UserProfile), typeof(DepositProfile));
        collection.AddScoped<UserService>();
        collection.AddScoped<DepositService>();
        collection.AddScoped<JwtService>();
        collection.AddScoped<AuthService>();
        return collection;
    }
}