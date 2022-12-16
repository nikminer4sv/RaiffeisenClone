using CurrencyProfiler.Domain;
using Microsoft.EntityFrameworkCore;

namespace CurrencyProfiler.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<CurrencyList> Currencies { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }
}