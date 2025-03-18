namespace OrderBookMonitor.Application.OrderBook.Models;

public class DepthOfMarketItemModel
{
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
    public int Count { get; set; }
}