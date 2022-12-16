using CurrencyProfiler.Domain.CommonViewModels;

namespace CurrencyProfiler.Application.ViewModels;

public class GridRequestViewModel
{
    public string Column { get; set; }
    public FilterViewModel? Condition1 { get; set; }
    public FilterViewModel? Condition2 { get; set; }
}