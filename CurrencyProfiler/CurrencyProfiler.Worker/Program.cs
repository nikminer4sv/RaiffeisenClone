using CurrencyProfiler.Application;
using CurrencyProfiler.Application.Interfaces;
using CurrencyProfiler.Application.Services;
using CurrencyProfiler.Domain;
using CurrencyProfiler.Persistence.Interfaces;
using CurrencyProfiler.Persistence.Services;
using CurrencyProfiler.Worker.Extensions;
using CurrencyProfiler.Worker.Services;
using CurrencyProfiler.Worker.Utils;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDbContext<CurrencyProfiler.Persistence.AppDbContext>(options =>
        {
            options.UseSqlServer("Server=localhost;Database=CurrencyProfiler;User=sa;Password=7514895263a-B", b => b.MigrationsAssembly("CurrencyProfiler.WebApi"));
        });
        services.AddScoped<IDbService<CurrencyList>, DbService>();
        services.AddSingleton<ICurrencyService, CurrencyService>();
        services.AddCronJob<CronJobWork>(new ScheduleConfig<CronJobWork>
        {
            TimeZoneInfo = TimeZoneInfo.Local,
            CronExpression = @"*/1 * * * *"
        });
    })
    .Build();

await host.RunAsync();