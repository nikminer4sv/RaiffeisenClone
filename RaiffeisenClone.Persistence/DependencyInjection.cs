using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RaiffeisenClone.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection collection, IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];
        collection.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        return collection;
    }
}