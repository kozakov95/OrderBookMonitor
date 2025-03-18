using Microsoft.Extensions.DependencyInjection;
using OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.Services;
using OrderBookMonitor.Infrastructure.Modules.OrderBook.HostedServices;

namespace OrderBookMonitor.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        
        services.AddSingleton<IOrderBookStreamingService, BitstampOrderBookStreamingService>();
        services.AddHostedService<OrderBookStreamingBackgroundService>();
        services.AddAutoMapper(
            typeof(Modules.OrderBook.Mapping.OrderBookProfile).Assembly);
        
        return services;
    }
}