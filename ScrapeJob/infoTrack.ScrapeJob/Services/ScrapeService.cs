using infoTrack.ScrapeJob.Models;
using HtmlAgilityPack;
using infoTrack.ScrapeJob.Client.SearchClient;

namespace infoTrack.ScrapeJob.Services
{
    public class ScrapeService: IScrapeService
    {
        private readonly ISearchClient _client;
        public ScrapeService(ISearchClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<MatchingResult>> SearchAndScrapeAsync(string searchPhrase, string matchingUrl)
        {
            var searchTimeStamp = DateTimeOffset.Now;
            var resultHtml = await _client.GetHtmlResult(searchPhrase);


            var scrubbedResults = ScrubHtml(resultHtml);

            return CheckMatching(scrubbedResults, searchPhrase, matchingUrl, searchTimeStamp);

        }

        private List<HtmlNode> ScrubHtml(string html)
        {
            var matchingResults = new List<MatchingResult>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            var mainElement = doc.GetElementbyId("main");

            var children = mainElement.ChildNodes;

            List<HtmlNode> searchResultElements = new List<HtmlNode>();

            searchResultElements = children.Where(x =>
            {
                if (x.Name != "div")
                    return false;
                else if (x.Descendants().Any(x => x.Name == "style"))
                    return false;
                else if (x.Descendants().Count() == 0)
                    return false;
                else if (x.InnerHtml.Contains("<!--") && x.InnerHtml.Contains("-->"))
                    return false;
                else
                    return true;
            }).ToList();

            return searchResultElements;
        }

        private List<MatchingResult> CheckMatching(List<HtmlNode> orderedResults, string searchPhrase, string matchingUrl, DateTimeOffset searchTimeStamp)
        {
            var matchingResults = new List<MatchingResult>();

            int resultRank = 1;

            foreach(HtmlNode result in orderedResults)
            {
                if (result.InnerHtml.Contains(matchingUrl))
                {
                    var match = new MatchingResult();
                    match.ResultRank = resultRank;
                    match.MatchingUrl = matchingUrl;
                    match.SearchPhrase = searchPhrase;
                    match.SearchSite = "https://www.google.com/";
                    match.SearchTimeStamp = searchTimeStamp;

                    matchingResults.Add(match);
                }

                resultRank++;
            }

            //if no matching results
            if (!matchingResults.Any())
            {
                var match = new MatchingResult();
                match.ResultRank = 0; //0 = no matching result
                match.MatchingUrl = matchingUrl;
                match.SearchPhrase = searchPhrase;
                match.SearchSite = "https://www.google.com/";
                match.SearchTimeStamp = searchTimeStamp;

                matchingResults.Add(match);
            }

            return matchingResults;
        }

    }
}
