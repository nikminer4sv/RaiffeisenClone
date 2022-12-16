using CurrencyProfiler.Worker.Interfaces;
using CurrencyProfiler.Worker.Services;
using CurrencyProfiler.Worker.Utils;

namespace CurrencyProfiler.Worker.Extensions;

public static class ScheduledServiceExtensions
{
    public static IServiceCollection AddCronJob<T>(this IServiceCollection services, IScheduleConfig<T> config) where T : CronJobService
    {
        if (config == null)
        {
            throw new ArgumentNullException(nameof(config), @"Please provide Schedule Configurations.");
        }
        if (string.IsNullOrWhiteSpace(config.CronExpression))
        {
            throw new ArgumentNullException(nameof(ScheduleConfig<T>.CronExpression), @"Empty Cron Expression is not allowed.");
        }

        services.AddSingleton<IScheduleConfig<T>>(config);
        services.AddHostedService<T>();
        return services;
    }
}