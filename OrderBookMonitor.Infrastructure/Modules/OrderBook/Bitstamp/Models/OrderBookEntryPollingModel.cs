using System.Text.Json.Serialization;
using OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.JsonConverters;

namespace OrderBookMonitor.Modules.OrderBook.CommonModels;

[JsonConverter(typeof(OrderBookEntryModelConverter))]
public class OrderBookEntryPollingModel
{
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
}
