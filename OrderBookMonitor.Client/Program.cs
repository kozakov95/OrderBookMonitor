using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace OrderBookMonitor.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Services
            .AddBlazorise()
            .AddBootstrapProviders()
            .AddFontAwesomeIcons();


        await builder.Build().RunAsync();
    }
}