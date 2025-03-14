using System.Text.Json.Serialization;

namespace OrderBookMonitor.Modules.OrderBook.CommonModels;

public class OrderBookModel
{
    [JsonPropertyName("data")]
    public OrderBookDataModel DataModel { get; set; }
    
    [JsonPropertyName("channel")]
    public string Channel { get; set; }
    
    [JsonPropertyName("event")]
    public string Event { get; set; }

}