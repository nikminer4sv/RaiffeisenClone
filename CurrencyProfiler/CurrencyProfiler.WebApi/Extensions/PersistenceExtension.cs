using CurrencyProfiler.Domain;
using CurrencyProfiler.Persistence.Interfaces;
using CurrencyProfiler.Persistence.Services;
using Microsoft.EntityFrameworkCore;

namespace CurrencyProfiler.WebApi.Extensions;

public static class PersistenceExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection collection, string connectionString)
    {
        collection.AddDbContext<CurrencyProfiler.Persistence.AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("CurrencyProfiler.WebApi"));
        });
        collection.AddScoped<IDbService<CurrencyList>, DbService>();
        return collection;
    }
}