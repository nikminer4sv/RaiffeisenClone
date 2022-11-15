using System.Text.Json.Serialization;

namespace CurrencyProfiler.Application.ViewModels;

public class CurrencyApiResponseViewModel
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    [JsonPropertyName("timestamp")]
    public int Timestamp { get; set; }
    [JsonPropertyName("base")]
    public string Base { get; set; }
    [JsonPropertyName("date")]
    public string Date { get; set; }
    [JsonPropertyName("rates")]
    public Dictionary<string, double> Rates { get; set; }
}