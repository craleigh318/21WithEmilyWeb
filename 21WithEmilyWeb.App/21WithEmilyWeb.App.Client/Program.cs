using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using _21WithEmilyWeb.App.Client.Services;

namespace _21WithEmilyWeb.App.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            var gameApiBaseUrl = builder.Configuration["GameApi:BaseUrl"];
            Uri.TryCreate(gameApiBaseUrl, UriKind.Absolute, out var gameApiBaseAddress);

            builder.Services.AddScoped(_ => new HttpClient { BaseAddress = gameApiBaseAddress });
            builder.Services.AddScoped<GameApiClient>();

            await builder.Build().RunAsync();
        }
    }
}
