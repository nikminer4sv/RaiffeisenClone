using CurrencyProfiler.Application;
using CurrencyProfiler.Application.Interfaces;
using CurrencyProfiler.Persistence.Interfaces;

namespace CurrencyProfiler.WebApi;

public class Worker : BackgroundService
{
    private readonly IDbService<CurrencyList> _dbService;
    private readonly ICurrencyService _currencyService;

    public Worker(IDbService<CurrencyList> dbService, ICurrencyService currencyService) =>
        (_dbService, _currencyService) = (dbService, currencyService);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var startTimeSpan = TimeSpan.Zero;
        var periodTimeSpan = TimeSpan.FromMinutes(5);

        var timer = new Timer(async (e) =>
        {
            var content = await _currencyService.GetExchangeRates("BYN", new string[]{"USD", "EUR", "RUB"});
                var currencyList = new CurrencyList
            {
                Currencies = content.Rates,
                Timestamp = content.Timestamp.ToString()
            };
            await _dbService.AddAsync(currencyList);
        }, null, startTimeSpan, periodTimeSpan);
    }
}