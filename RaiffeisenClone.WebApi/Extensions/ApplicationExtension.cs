using RaiffeisenClone.Application.MappingProfiles;

namespace RaiffeisenClone.WebApi.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddAutoMapper(typeof(UserProfile), typeof(DepositProfile));
        return collection;
    }
}