namespace OrderBookMonitor.Application.Modules.OrderBook.Models;

public class OrderBookModel
{
    public string Timestamp { get; set; }
    public List<OrderBookEntryItemModel> Bids { get; set; }
    public List<OrderBookEntryItemModel> Asks { get; set; }
}