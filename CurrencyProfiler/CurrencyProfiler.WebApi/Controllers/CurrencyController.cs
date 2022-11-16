using CurrencyProfiler.Application;
using CurrencyProfiler.Application.Interfaces;
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
        return await _dbService.GetLastAsync();
    }
}