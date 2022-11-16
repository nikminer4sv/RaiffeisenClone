using CurrencyProfiler.Worker.Interfaces;

namespace CurrencyProfiler.Worker.Utils;

public class ScheduleConfig<T> : IScheduleConfig<T>
{
    public string CronExpression { get; set; }
    public TimeZoneInfo TimeZoneInfo { get; set; }
}