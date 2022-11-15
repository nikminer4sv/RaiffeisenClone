using CurrencyProfiler.Application;
using CurrencyProfiler.Persistence.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CurrencyProfiler.Persistence.Services;

public class DbService : IDbService<CurrencyList>
{
    private readonly IMongoCollection<CurrencyList> _currencyHistory;

    public DbService(IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("mongo");
        var connection = new MongoUrlBuilder(connectionString);
        MongoClient client = new MongoClient(connectionString);
        IMongoDatabase database = client.GetDatabase(connection.DatabaseName);
        _currencyHistory = database.GetCollection<CurrencyList>("CurrencyHistory");
    }

    public async Task AddAsync(CurrencyList entity)
    {
        await _currencyHistory.InsertOneAsync(entity);
    }
}