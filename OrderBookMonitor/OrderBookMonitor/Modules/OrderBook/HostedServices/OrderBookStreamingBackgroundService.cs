using OrderBookMonitor.Application.OrderBook;

namespace OrderBookMonitor.Modules.OrderBook.HostedServices;

public class OrderBookStreamingBackgroundService: IHostedService
{
    private readonly IOrderBookStreamingService _streamingService;
    public OrderBookStreamingBackgroundService(IOrderBookStreamingService streamingService)
    {
        _streamingService = streamingService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _streamingService.StartStreamingAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _streamingService.StopStreamingAsync(cancellationToken);
    }
}