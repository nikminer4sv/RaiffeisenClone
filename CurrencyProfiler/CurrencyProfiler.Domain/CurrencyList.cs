using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CurrencyProfiler.Application;

[BsonNoId]
[BsonIgnoreExtraElements]
public class CurrencyList
{
    [BsonElement("Currencies")]
    public Dictionary<string, double>? Currencies { get; set; }
    [BsonElement("Timestamp")]
    public string? Timestamp { get; set; }
}