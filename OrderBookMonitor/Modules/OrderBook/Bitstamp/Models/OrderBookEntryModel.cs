using System.Text.Json.Serialization;
using OrderBookMonitor.Modules.OrderBook.Bitstamp.JsonConverters;

namespace OrderBookMonitor.Modules.OrderBook.Bitstamp.Models;

[JsonConverter(typeof(OrderBookEntryModelConverter))]
public class OrderBookEntryModel
{
    public string Price { get; set; }
    public string Amount { get; set; }
}