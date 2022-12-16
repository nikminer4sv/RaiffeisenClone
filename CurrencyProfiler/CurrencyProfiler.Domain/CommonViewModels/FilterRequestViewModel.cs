using CurrencyProfiler.Domain.CommonViewModels;

namespace CurrencyProfiler.Domain.CommonViewModels;

public class FilterRequestViewModel
{
    public string ColId { get; set; }
    public string FilterType { get; set; }
    public string? Type { get; set; }
    public string? Filter { get; set; }
    public string? Operator { get; set; }
    public FilterViewModel? Condition1 { get; set; }
    public FilterViewModel? Condition2 { get; set; }
} 