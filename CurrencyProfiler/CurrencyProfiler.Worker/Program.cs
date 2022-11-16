using CurrencyProfiler.Application;
using CurrencyProfiler.Application.Interfaces;
using CurrencyProfiler.Application.Services;
using CurrencyProfiler.Persistence.Interfaces;
using CurrencyProfiler.Persistence.Services;
using CurrencyProfiler.Worker.Extensions;
using CurrencyProfiler.Worker.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IDbService<CurrencyList>, DbService>();
        services.AddSingleton<ICurrencyService, CurrencyService>();
        services.AddCronJob<CronJobWork>(c =>
        {
            c.TimeZoneInfo = TimeZoneInfo.Local;
            c.CronExpression = @"*/1 * * * *";
        });
    })
    .Build();

await host.RunAsync();