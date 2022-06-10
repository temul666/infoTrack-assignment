namespace infoTrack.ScrapeJob.Client.SearchClient
{
    public interface ISearchClient
    {
        Task<string> GetHtmlResult(string searchPhrase);
    }
}
