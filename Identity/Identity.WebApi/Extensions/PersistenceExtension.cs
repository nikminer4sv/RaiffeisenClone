using Identity.Persistence;
using Identity.Persistence.Interfaces;
using Identity.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Identity.WebApi.Extensions;

public static class PersistenceExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection collection, string connectionString)
    {
        collection.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Identity.WebApi"));
        });
        collection.AddTransient<IUserRepository, UserRepository>();
        collection.AddTransient<IAvatarRepository, AvatarRepository>();
        
        return collection;
    }
}