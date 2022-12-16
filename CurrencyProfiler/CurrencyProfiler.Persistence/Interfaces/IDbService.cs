using CurrencyProfiler.Domain;
using CurrencyProfiler.Domain.CommonViewModels;
using CurrencyProfiler.Persistence.ViewModels;

namespace CurrencyProfiler.Persistence.Interfaces;

public interface IDbService<T>
{
    Task<CurrencyViewModel> GetLastAsync();
    Task AddAsync(CurrencyList currencyList);

    Task<(List<CurrencyViewModel>, int)> GetByParameters(SortViewModel? sortViewModel, List<FilterRequestViewModel>? filterViewModel, int start, int count);

    int Count();
}