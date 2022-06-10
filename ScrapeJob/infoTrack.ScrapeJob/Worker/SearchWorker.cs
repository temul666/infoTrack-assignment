using infoTrack.ScrapeJob.Client.ApiClient;
using infoTrack.ScrapeJob.Services;

namespace infoTrack.ScrapeJob.Worker
{
    public class SearchWorker: ISearchWorker
    {
        private readonly IScrapeService _scrapeService;
        private readonly IApiClient _apiClient;
        public SearchWorker(IScrapeService scrapeService, IApiClient apiClient)
        {
            _scrapeService = scrapeService;
            _apiClient = apiClient;
        }

        public async Task DoWorkAsync(string searchPhrase, string matchingUrl)
        {
            var matchingResults = await _scrapeService.SearchAndScrapeAsync(searchPhrase, matchingUrl);

            if (matchingResults.Any())
            {
                await _apiClient.SaveResults(matchingResults.ToList());
            }

            
        }


    }
}
