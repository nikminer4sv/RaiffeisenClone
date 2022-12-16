using CurrencyProfiler.Persistence.ViewModels;

namespace CurrencyProfiler.Application.ViewModels;

public class CurrencyHistoryResponse
{
    public List<CurrencyViewModel> Currencies { get; set; }
    public int LastElement { get; set; }
}