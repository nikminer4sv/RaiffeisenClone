using System.Text.Json;
using CurrencyProfiler.Application.Interfaces;
using CurrencyProfiler.Application.ViewModels;
using Microsoft.Extensions.Configuration;

namespace CurrencyProfiler.Application.Services;

public class CurrencyService : ICurrencyService
{
    public readonly IConfiguration _configuration;

    public CurrencyService(IConfiguration configuration) =>
        (_configuration) = (configuration);
    public async Task<CurrencyApiResponseViewModel> GetExchangeRates(string baseCurrency, string[] currencies)
    {
        using (var client = new HttpClient())
        {
            string requestCurrencies = String.Join(',', currencies);
            client.DefaultRequestHeaders.Add("apikey", _configuration["ApiToken"]);
            var result = await client.GetStringAsync($"https://api.apilayer.com/exchangerates_data/latest?base={baseCurrency}&symbols={requestCurrencies}");
            var responseViewModel = JsonSerializer.Deserialize<CurrencyApiResponseViewModel>(result);
            return responseViewModel;
        }
    } 
}