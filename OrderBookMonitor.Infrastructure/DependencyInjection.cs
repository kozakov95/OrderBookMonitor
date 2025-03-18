using Microsoft.Extensions.DependencyInjection;
using OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.Services;
using OrderBookMonitor.Infrastructure.Modules.OrderBook.HostedServices;
using Serilog;

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
        
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(
                path: "logs/OrderBookMonitor.log", 
                rollingInterval: RollingInterval.Hour,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
            )
            .CreateLogger();

        
        return services;
    }
}