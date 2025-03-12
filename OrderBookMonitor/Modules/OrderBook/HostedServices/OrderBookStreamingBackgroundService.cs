using OrderBookMonitor.Application.OrderBook;

namespace OrderBookMonitor.Modules.OrderBook.HostedServices;

public class OrderBookStreamingBackgroundService: BackgroundService
{
    private readonly IOrderBookStreamingService _streamingService;
    public OrderBookStreamingBackgroundService(IOrderBookStreamingService streamingService)
    {
        _streamingService = streamingService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await _streamingService.StartStreamingAsync(stoppingToken);
        }
        finally
        {
            await _streamingService.StopStreamingAsync(stoppingToken);
        }
    }
}