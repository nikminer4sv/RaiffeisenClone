using System.Text.Json;
using CurrencyProfiler.Application;
using CurrencyProfiler.Application.Interfaces;
using CurrencyProfiler.Application.ViewModels;
using CurrencyProfiler.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyProfiler.WebApi.Controllers;

[ApiController]
public class CurrencyController : BaseController
{
    private readonly ICurrencyService _currencyService;
    private readonly IDbService<CurrencyList> _dbService;

    public CurrencyController(ICurrencyService currencyService, IDbService<CurrencyList> dbService) => 
        (_currencyService, _dbService) = (currencyService, dbService);
    
    [HttpGet]
    [Route("getexchangerates")]
    public async Task<string> Get([FromQuery] string baseCurrency, [FromQuery] string[] currencies)
    {
        /*
        var content = await _currencyService.GetExchangeRates(baseCurrency, currencies);
        var currencyList = new CurrencyList
        {
            Currencies = content.Rates,
            Timestamp = content.Timestamp.ToString()
        };
        await _dbService.AddAsync(currencyList);
        return JsonSerializer.Serialize(content.Rates);
        */

        return await _dbService.GetLastAsync();
    }
}