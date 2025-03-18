namespace OrderBookMonitor.Application.OrderBook.Models;

public class DepthOfMarketModel
{
    public decimal Price { get; set; }
    public List<DepthOfMarketItemModel> Asks { get; set; }
    public List<DepthOfMarketItemModel> Bids { get; set; }
}