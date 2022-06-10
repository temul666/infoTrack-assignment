using infoTrack.ScrapeJob;
using infoTrack.ScrapeJob.Client.ApiClient;
using infoTrack.ScrapeJob.Client.SearchClient;
using infoTrack.ScrapeJob.Services;
using infoTrack.ScrapeJob.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

class Program
{
    public static async Task Main(string[] args)
    {
        var hostBuilder = new HostBuilder()
            .ConfigureAppConfiguration((hostContext, configBuilder) =>
            {
                configBuilder.SetBasePath(Directory.GetCurrentDirectory());
                configBuilder.AddJsonFile("appsettings.json", optional: true);
                configBuilder.AddEnvironmentVariables();
            })
            .ConfigureServices((hostContext, services) =>
            {

                services.AddHttpClient();
                services.AddScoped<IApiClient, ApiClient>();
                services.AddScoped<ISearchClient, GoogleClient>();
                services.AddScoped<IScrapeService, ScrapeService>();
                services.AddScoped<ISearchWorker, SearchWorker>();
                services.AddScoped<IHostedService, IntervalHostedService>();
                
                
            })
            .ConfigureLogging((hostContext, logging) =>
            {
                logging.AddJsonConsole();
            })
            .UseConsoleLifetime();

        await hostBuilder.RunConsoleAsync();
    }
}