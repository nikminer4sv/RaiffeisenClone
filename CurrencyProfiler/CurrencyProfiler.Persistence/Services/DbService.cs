using CurrencyProfiler.Domain;
using CurrencyProfiler.Domain.CommonViewModels;
using CurrencyProfiler.Persistence.Interfaces;
using CurrencyProfiler.Persistence.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CurrencyProfiler.Persistence.Services;

public class DbService : IDbService<CurrencyList>
{
    private readonly AppDbContext _context;
    
    public DbService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<CurrencyViewModel> GetLastAsync()
    {
        var record =
            await _context.Currencies.FirstOrDefaultAsync(c => c.Id == _context.Currencies.Max(c => c.Id));
        return new CurrencyViewModel
        {
            Usd = record.Usd,
            Eur = record.Eur,
            Rub = record.Rub,
            Timestamp = record.Timestamp
        };
    }

    public async Task AddAsync(CurrencyList currencyList)
    {
        await _context.Currencies.AddAsync(currencyList);
        await _context.SaveChangesAsync();
    }

    public async Task<(List<CurrencyViewModel>, int)> GetByParameters(SortViewModel? sortViewModel,
        List<FilterRequestViewModel>? filterViewModel, int start, int count)
    {
        var sortPart = "";
        if (sortViewModel is not null)
        {
            sortPart = $" ORDER BY {sortViewModel.ColId} {sortViewModel.Sort}";
        }

        var filterPart = "";
        if (filterViewModel is not null)
        {
            filterPart += "WHERE ";
            foreach (var filter in filterViewModel)
            {
                if (filter.Condition1 is null)
                {
                    filterPart += $"{ConvertFromGridFilterToSqlFilter(filter.Type, filter.Filter, filter.ColId)} AND ";
                }
                else
                {
                    filterPart += $"" +
                                 $"{ConvertFromGridFilterToSqlFilter(filter.Condition1.Type, filter.Condition1.Filter, filter.ColId)} " +
                                 $"{filter.Operator} " +
                                 $"{ConvertFromGridFilterToSqlFilter(filter.Condition2.Type, filter.Condition2.Filter, filter.ColId)} AND ";
                }
            }

            if (filterPart.EndsWith("AND "))
            {
                filterPart = filterPart.Substring(0, filterPart.Length - 4);
            }
        }

        //var content = _context.Currencies.FromSqlRaw($";with tbl as (SELECT ROW_NUMBER() over(order by(select 1)) as RowIndex,* from Currencies) select top {start + count} * from tbl where RowIndex>={start} {filterPart} {sortPart}").ToList();
        var content = _context.Currencies.FromSqlRaw($"SELECT * FROM Currencies {filterPart} {sortPart}").ToList();

        var result = new List<CurrencyViewModel>();
        for (var i = start; i < (start + count <= content.Count ? start + count : content.Count); i++)
        {
            result.Add(new CurrencyViewModel
            {
                Usd = content[i].Usd,
                Eur = content[i].Eur,
                Rub = content[i].Rub,
                Timestamp = content[i].Timestamp
            });
        }

        return (result, content.Count);
    }

    public int Count() => _context.Currencies.Count();

    private static string ConvertFromGridFilterToSqlFilter(string filterType, string? value, string colId)
    {
        switch (filterType)
        {
            case "startsWith":
            {
                return $"TRIM(CAST({colId} AS NVARCHAR)) LIKE '{value}%'";
            }
            case "endsWith":
            {
                return $"TRIM(CAST({colId} AS NVARCHAR)) LIKE '%{value}'";
            }
            case "contains":
            {
                return $"CHARINDEX('{value}', {colId}) > 0";
            }
            case "notContains":
            {
                return $"CHARINDEX('{value}', {colId}) = 0";
            }
            case "equals":
            {
                return $"{colId} = '{value}'";
            }
            case "notEqual":
            {
                return $"{colId} != '{value}'";
            }
            case "blank":
            {
                return $"{colId} = ''";
            }
            case "notBlank":
            {
                return $"{colId} != ''";
            }
        }

        throw new Exception("Invalid type");
    }
}