using Microsoft.AspNetCore.WebSockets;
using OrderBookMonitor.Application;
using OrderBookMonitor.Common.Modules.OrderBook.Constants;
using OrderBookMonitor.Components;
using OrderBookMonitor.Infrastructure;
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

        builder.Services.AddRazorPages();

        builder.Services.AddWebSockets(configure => 
        {
            configure.KeepAliveInterval = TimeSpan.FromSeconds(5);
        });

        builder.Services.RegisterApplicationServices();
        builder.Services.RegisterInfrastructureServices();

        
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