using CurrencyProfiler.Application;
using CurrencyProfiler.Application.Interfaces;
using CurrencyProfiler.Persistence.Interfaces;
using CurrencyProfiler.Worker.Interfaces;

namespace CurrencyProfiler.Worker.Services;

public class CronJobWork : CronJobService
{
    private readonly ILogger<CronJobWork> _logger;
    private readonly IDbService<CurrencyList> _dbService;
    private readonly ICurrencyService _currencyService;

    public CronJobWork(IScheduleConfig<CronJobWork> config, ILogger<CronJobWork> logger, IDbService<CurrencyList> dbService, ICurrencyService currencyService)
        : base(config.CronExpression, config.TimeZoneInfo)
    {
        _logger = logger;
        _dbService = dbService;
        _currencyService = currencyService;
    }
    
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Currency mongo worker start");
        return base.StartAsync(cancellationToken);
    }

    public override async Task DoWork(CancellationToken cancellationToken)
    {
        var content = await _currencyService.GetExchangeRates("BYN", new string[]{"USD", "EUR", "RUB"});
        var currencyList = new CurrencyList
        {
            Currencies = content.Rates,
            Timestamp = content.Timestamp.ToString()
        };
        await _dbService.AddAsync(currencyList);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Currency mongo worker is stopping");
        return base.StopAsync(cancellationToken);
    }
}