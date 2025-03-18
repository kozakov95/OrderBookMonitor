using CSharpFunctionalExtensions;
using MediatR;
using OrderBookMonitor.Application.Modules.OrderBook.Models;

namespace OrderBookMonitor.Application.Modules.OrderBook.Queries.CalculateDepthOfMarket;

public class CalculateDepthOfMarketQuery : IRequest<Result<DepthOfMarketModel>>
{
    public OrderBookModel OrderBook { get; set; }

    public CalculateDepthOfMarketQuery(OrderBookModel orderBook)
    {
        OrderBook = orderBook;
    }
}