using CurrencyProfiler.Application.ViewModels;

namespace CurrencyProfiler.Application.Interfaces;

public interface ICurrencyService
{ 
    Task<CurrencyApiResponseViewModel> GetExchangeRates(string baseCurrency, string[] currencies);
}