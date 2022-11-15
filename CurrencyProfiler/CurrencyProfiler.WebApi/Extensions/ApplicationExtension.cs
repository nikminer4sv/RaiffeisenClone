using CurrencyProfiler.Application;
using CurrencyProfiler.Application.Interfaces;
using CurrencyProfiler.Application.Services;
using CurrencyProfiler.Persistence.Interfaces;

namespace CurrencyProfiler.WebApi.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddSingleton<ICurrencyService, CurrencyService>();
        return collection;
    }
}