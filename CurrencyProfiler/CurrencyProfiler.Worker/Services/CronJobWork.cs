using CurrencyProfiler.Application;
using CurrencyProfiler.Application.Interfaces;
using CurrencyProfiler.Domain;
using CurrencyProfiler.Persistence;
using CurrencyProfiler.Persistence.Interfaces;
using CurrencyProfiler.Worker.Interfaces;

namespace CurrencyProfiler.Worker.Services;

public class CronJobWork : CronJobService
{
    private readonly ILogger<CronJobWork> _logger;
    private readonly ICurrencyService _currencyService;
    private readonly IServiceProvider _serviceProvider;

    public CronJobWork(IServiceProvider serviceProvider, IScheduleConfig<CronJobWork> config, ILogger<CronJobWork> logger, ICurrencyService currencyService)
        : base(config.CronExpression, config.TimeZoneInfo)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _currencyService = currencyService;
    }
    
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Currency sql worker start");
        return base.StartAsync(cancellationToken);
    }

    public override async Task DoWork(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        
        var dbService = scope.ServiceProvider.GetService<IDbService<CurrencyList>>();
        
        var content = await _currencyService.GetExchangeRates("BYN", new string[]{"USD", "EUR", "RUB"});
        var currencyList = new CurrencyList
        {
            Usd = content.Rates["USD"],
            Eur = content.Rates["EUR"],
            Rub = content.Rates["RUB"],
            Timestamp = content.Timestamp.ToString()
        };
        await dbService.AddAsync(currencyList);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Currency sql worker is stopping");
        return base.StopAsync(cancellationToken);
    }
}