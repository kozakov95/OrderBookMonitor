using Microsoft.AspNetCore.WebSockets;
using OrderBookMonitor.Application.OrderBook;
using OrderBookMonitor.Components;
using OrderBookMonitor.Modules.OrderBook.Bitstamp.Services;
using OrderBookMonitor.Modules.OrderBook.HostedServices;

namespace OrderBookMonitor;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        builder.Services.AddWebSockets(configure => 
        {
            configure.KeepAliveInterval = TimeSpan.FromSeconds(5);
            configure.AllowedOrigins.Add("wss://ws.bitstamp.net");
        });
        builder.Services.AddSignalR();
        
        builder.Services.AddSingleton<IOrderBookStreamingService, BitstampOrderBookStreamingService>();
        builder.Services.AddHostedService<OrderBookStreamingBackgroundService>();


        
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
        app.UseWebSockets();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        app.Run();
    }
}