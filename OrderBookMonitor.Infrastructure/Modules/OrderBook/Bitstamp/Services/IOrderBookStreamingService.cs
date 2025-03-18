using OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.Models;

namespace OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.Services;

public interface IOrderBookStreamingService
{
    Task StartStreamingAsync(CancellationToken cancellationToken);
    
    Task StopStreamingAsync(CancellationToken cancellationToken);
    
    event Func<OrderBookPollingModel, Task>? OnDataReceived;
}
