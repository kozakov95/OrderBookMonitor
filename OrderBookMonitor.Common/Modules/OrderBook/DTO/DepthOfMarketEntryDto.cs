namespace OrderBookMonitor.Common.Modules.OrderBook.DTO;

public class DepthOfMarketEntryDto
{
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
    public int Count { get; set; }
}