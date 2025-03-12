using System.Text.Json.Serialization;

namespace OrderBookMonitor.Modules.OrderBook.Bitstamp.Models;

public class OrderBookDataModel
{
    [JsonPropertyName("timestamp")]
    public string Timestamp { get; set; }
    
    [JsonPropertyName("microtimestamp")]
    public string Microtimestamp { get; set; }
    
    [JsonPropertyName("bids")]
    public List<OrderBookEntryModel> Bids { get; set; }
    
    [JsonPropertyName("asks")]
    public List<OrderBookEntryModel> Asks { get; set; }
}