using CurrencyProfiler.Application;
using CurrencyProfiler.Persistence.Interfaces;
using CurrencyProfiler.Persistence.Services;

namespace CurrencyProfiler.WebApi.Extensions;

public static class PersistenceExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection collection)
    {
        collection.AddSingleton<IDbService<CurrencyList>, DbService>();
        return collection;
    }
}