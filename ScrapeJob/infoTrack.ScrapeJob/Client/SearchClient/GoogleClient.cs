namespace infoTrack.ScrapeJob.Client.SearchClient
{
    public class GoogleClient : ISearchClient
    {
        private readonly IHttpClientFactory _clientFactory;
        public GoogleClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<string> GetHtmlResult(string searchPhrase)
        {
            var client = _clientFactory.CreateClient();
            searchPhrase = searchPhrase.Replace(" ", "+");

            string searchUrl = string.Format("{0}{1}={2}&{3}={4}", "https://www.google.com/search?", "num", "100", "q", searchPhrase);

            return await client.GetStringAsync(searchUrl);
        }
    }
}
