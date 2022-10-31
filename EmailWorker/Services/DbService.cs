using System.ComponentModel.DataAnnotations.Schema;
using EmailWorker.Entities;
using EmailWorker.Interfaces;
using MongoDB.Driver;
using RabbitMQ.Client;

namespace EmailWorker.Services;

public class DbService : IDbService<EmailDto>
{
    private readonly IMongoCollection<EmailDto> _products;

    public DbService(IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("mongo");
        var connection = new MongoUrlBuilder(connectionString);
        MongoClient client = new MongoClient(connectionString);
        IMongoDatabase database = client.GetDatabase(connection.DatabaseName);
        _products = database.GetCollection<EmailDto>("emails");
    }

    public async Task Add(EmailDto entity)
    {
        await _products.InsertOneAsync(entity);
    }
}