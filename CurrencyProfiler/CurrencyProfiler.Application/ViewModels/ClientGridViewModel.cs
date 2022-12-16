using CurrencyProfiler.Domain.CommonViewModels;

namespace CurrencyProfiler.Application.ViewModels;

public class ClientGridViewModel
{
    public SortViewModel? SortViewModel { get; set; }
    public List<FilterRequestViewModel>? FilterViewModel { get; set; }
}