using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CurrencyProfiler.Domain;

public class CurrencyList
{
    public int Id { get; set; }
    public double Usd { get; set; }
    public double Eur { get; set; }
    public double Rub { get; set; }
    public string? Timestamp { get; set; }
}