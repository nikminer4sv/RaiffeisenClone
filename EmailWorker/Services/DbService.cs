using EmailWorker.Entities;
using MongoDB.Driver;

namespace EmailWorker.Services;

public class DbService
{
    private readonly IMongoCollection<EmailDto> _products;

    public DbService()
    {
        string connectionString = "mongodb://mongodb/raiffeisen";
        var connection = new MongoUrlBuilder(connectionString);
        MongoClient client = new MongoClient(connectionString);
        IMongoDatabase database = client.GetDatabase(connection.DatabaseName);
        _products = database.GetCollection<EmailDto>("emails");
    }

    public async Task Add(string email)
    {
        await _products.InsertOneAsync(new EmailDto() {Email = email});
    }
}