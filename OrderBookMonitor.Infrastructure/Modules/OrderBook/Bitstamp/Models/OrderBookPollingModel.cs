using System.Text.Json.Serialization;

namespace OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.Models;

public class OrderBookPollingModel
{
    [JsonPropertyName("data")]
    public OrderBookDataPollingModel DataPollingModel { get; set; }
    
    [JsonPropertyName("channel")]
    public string Channel { get; set; }
    
    [JsonPropertyName("event")]
    public string Event { get; set; }
}
