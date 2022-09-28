using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RaiffeisenClone.Application.MappingProfiles;

namespace RaiffeisenClone.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddAutoMapper(typeof(UserProfile));
        return collection;
    }
}