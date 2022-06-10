using infoTrack.ScrapeJob.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using infoTrack.ScrapeJob.Models;
using Timer = System.Threading.Timer;

namespace infoTrack.ScrapeJob
{
    public class IntervalHostedService: IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private ISearchWorker _searchWorker;
        private JobSettings settings;

        public IntervalHostedService(ILogger<IntervalHostedService> logger, ISearchWorker searchWorker, IConfiguration config)
        {
            _logger = logger;
            _searchWorker = searchWorker;
            settings = config.GetSection("JobSettings").Get<JobSettings>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service is starting.");

            _logger.LogInformation("api url: "  + settings.ApiUrl + ":" + settings.ApiPort);

            _timer = new Timer(RunOnInterval, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(settings.IntevalInSeconds));

            return Task.CompletedTask;
        }

        private void RunOnInterval(object state)
        {
            _logger.LogInformation("Service is running.");
            _searchWorker.DoWorkAsync(settings.SearchPhrase, settings.MatchingUrl);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
