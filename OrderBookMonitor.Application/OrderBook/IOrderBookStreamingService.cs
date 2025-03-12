namespace OrderBookMonitor.Application.OrderBook;

public interface IOrderBookStreamingService
{
    Task StartStreamingAsync(CancellationToken cancellationToken);
    
    Task StopStreamingAsync(CancellationToken cancellationToken);
}