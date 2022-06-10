using infoTrack.ScrapeJob.Models;

namespace infoTrack.ScrapeJob.Client.ApiClient
{
    public interface IApiClient
    {
        Task SaveResults(List<MatchingResult> results);
    }
}
