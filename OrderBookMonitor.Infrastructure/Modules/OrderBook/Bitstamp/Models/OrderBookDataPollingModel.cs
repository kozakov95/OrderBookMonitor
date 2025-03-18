using System.Text.Json.Serialization;

namespace OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.Models;

public class OrderBookDataPollingModel
{
    [JsonPropertyName("timestamp")]
    public string Timestamp { get; set; }
    
    [JsonPropertyName("microtimestamp")]
    public string Microtimestamp { get; set; }
    
    [JsonPropertyName("bids")]
    public List<OrderBookEntryPollingModel> Bids { get; set; }
    
    [JsonPropertyName("asks")]
    public List<OrderBookEntryPollingModel> Asks { get; set; }
}
