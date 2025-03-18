namespace OrderBookMonitor.Application.OrderBook.Models;

public class OrderBookModel
{
    public List<OrderBookEntryItemModel> Bids { get; set; }
    
    
    public List<OrderBookEntryItemModel> Asks { get; set; }
}