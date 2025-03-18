namespace OrderBookMonitor.Common.Modules.OrderBook.DTO;

public class DepthOfMarketDto
{
    public decimal Price { get; set; }
    public List<DepthOfMarketEntryDto> Asks { get; set; }
    public List<DepthOfMarketEntryDto> Bids { get; set; }
}