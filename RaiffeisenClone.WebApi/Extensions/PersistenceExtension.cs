using Microsoft.EntityFrameworkCore;
using RaiffeisenClone.Persistence;
using RaiffeisenClone.Persistence.Interfaces;
using RaiffeisenClone.Persistence.Repositories;

namespace RaiffeisenClone.WebApi.Extensions;

public static class PersistenceExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection collection, string connectionString)
    {
        collection.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("RaiffeisenClone.WebApi"));
        });
        collection.AddTransient<IUnitOfWork, UnitOfWork>();
        
        return collection;
    }
}