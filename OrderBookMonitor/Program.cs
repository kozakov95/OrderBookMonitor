using ApexCharts;
using Microsoft.AspNetCore.WebSockets;
using OrderBookMonitor.Application;
using OrderBookMonitor.Common.Modules.OrderBook.Constants;
using OrderBookMonitor.Components;
using OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.Services;
using OrderBookMonitor.Infrastructure.Modules.OrderBook.HostedServices;
using OrderBookMonitor.Infrastructure.Modules.OrderBook.Messaging.Hubs;

namespace OrderBookMonitor;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();
        
        builder.Services.AddServerSideBlazor();
        builder.Services.AddApexCharts();

        builder.Services.AddRazorPages();

        builder.Services.AddWebSockets(configure => 
        {
            configure.KeepAliveInterval = TimeSpan.FromSeconds(5);
        });

        builder.Services.AddSingleton<IOrderBookStreamingService, BitstampOrderBookStreamingService>();
        builder.Services.AddHostedService<OrderBookStreamingBackgroundService>();
        builder.Services.AddAutoMapper(
            typeof(OrderBookMonitor.Infrastructure.Modules.OrderBook.Mapping.OrderBookProfile).Assembly);

        builder.Services.RegisterApplicationServices();

        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        
        app.UseHttpsRedirection();
        
        app.UseStaticFiles();
        
        app.UseRouting();
        
        app.UseAntiforgery();
        
        app.UseWebSockets();
        
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        app.MapHub<OrderBookHub>(SignalRMethodConstants.OrderBookStreaming);

        app.Run();
    }
}