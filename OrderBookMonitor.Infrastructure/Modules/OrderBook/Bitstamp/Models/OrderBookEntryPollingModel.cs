using System.Text.Json.Serialization;
using OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.JsonConverters;

namespace OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.Models;

[JsonConverter(typeof(OrderBookEntryModelConverter))]
public class OrderBookEntryPollingModel
{
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
}
