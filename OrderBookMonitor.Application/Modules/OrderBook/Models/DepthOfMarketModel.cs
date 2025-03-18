namespace OrderBookMonitor.Application.Modules.OrderBook.Models;

public class DepthOfMarketModel
{
    public string Timestamp { get; set; }
    public decimal Price { get; set; }
    public List<DepthOfMarketItemModel> Asks { get; set; }
    public List<DepthOfMarketItemModel> Bids { get; set; }
}