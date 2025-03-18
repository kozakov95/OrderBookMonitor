using OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.Models;
using OrderBookMonitor.Modules.OrderBook.CommonModels;

namespace OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.Services;

public interface IOrderBookStreamingService
{
    Task StartStreamingAsync(CancellationToken cancellationToken);
    
    Task StopStreamingAsync(CancellationToken cancellationToken);
    
    event Func<OrderBookPollingModel, Task>? OnDataReceived;
}
