using CSharpFunctionalExtensions;
using MediatR;
using OrderBookMonitor.Application.Modules.OrderBook.Models;

namespace OrderBookMonitor.Application.Modules.OrderBook.Queries.CalculateDepthOfMarket;

public class CalculateDepthOfMarketQueryHandler : IRequestHandler<CalculateDepthOfMarketQuery, Result<DepthOfMarketModel>>
{
    
    
    public async Task<Result<DepthOfMarketModel>> Handle(CalculateDepthOfMarketQuery request, CancellationToken cancellationToken)
    {
        if (!request.OrderBook.Bids.Any() || !request.OrderBook.Asks.Any())
            Result.Failure<DepthOfMarketModel>("Order book is empty.");
        
        var calculatedPrice = CalculateMarketPrice(request.OrderBook);
        var domAsks = CalculateNonCumulativeDom(request.OrderBook.Asks, orderByAscending: false);
        var domBids = CalculateNonCumulativeDom(request.OrderBook.Bids, orderByAscending: true);

        return new DepthOfMarketModel
        {
            Timestamp = request.OrderBook.Timestamp,
            Price = calculatedPrice,
            Asks = domAsks,
            Bids = domBids,
        };
    }
    
    
    private decimal CalculateMarketPrice(OrderBookModel orderBook)
    {
        var bestBid = orderBook.Bids.Max(b => b.Price);
        var bestAsk = orderBook.Asks.Min(a => a.Price);

        return (bestBid + bestAsk) / 2m;
    }
    
    public static List<DepthOfMarketItemModel> CalculateNonCumulativeDom(IEnumerable<OrderBookEntryItemModel> orderBookData, bool orderByAscending)
    {
        var domEntries = orderBookData
            .GroupBy(bid => bid.Price)
            .Select(group => new DepthOfMarketItemModel
            {
                Price = group.Key,
                Amount = group.Sum(entry => entry.Amount),
                Count = group.Count()
            });

        return orderByAscending ? 
            domEntries.OrderBy(x => x.Price).ToList() :
            domEntries.OrderByDescending(x => x.Price).ToList();
    }
    
}