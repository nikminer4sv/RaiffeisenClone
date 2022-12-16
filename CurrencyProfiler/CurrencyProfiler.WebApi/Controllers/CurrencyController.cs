using System.Globalization;
using System.Text.Json;
using CurrencyProfiler.Application.Interfaces;
using CurrencyProfiler.Application.ViewModels;
using CurrencyProfiler.Domain;
using CurrencyProfiler.Persistence.Interfaces;
using CurrencyProfiler.Persistence.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyProfiler.WebApi.Controllers;

[ApiController]
public class CurrencyController : BaseController
{
    private readonly ICurrencyService _currencyService;
    private readonly IDbService<CurrencyList> _dbService;
    private readonly ILogger<CurrencyController> _logger;
    private readonly HttpClient _httpClient;

    public CurrencyController(ICurrencyService currencyService, IDbService<CurrencyList> dbService, ILogger<CurrencyController> logger, HttpClient httpClient) => 
        (_currencyService, _dbService, _logger, _httpClient) = (currencyService, dbService, logger, httpClient);

    [HttpGet]
    [Route("getlast")]
    public async Task<CurrencyViewModel> GetLast()
    {
        return await _dbService.GetLastAsync();
    } 
    
    [HttpPost]
    [Route("get")]
    public async Task<CurrencyHistoryResponse> GetAll([FromQuery] int start, [FromQuery] int count, [FromBody] ClientGridViewModel clientGridViewModel)
    {
        (var content, count) =
            await _dbService.GetByParameters(clientGridViewModel.SortViewModel, clientGridViewModel.FilterViewModel, start, count);
        return new CurrencyHistoryResponse
        {
            Currencies = content,
            LastElement = count
        };
    }
}