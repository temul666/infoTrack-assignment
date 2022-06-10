namespace infoTrack.SearchResult.Core.Dto
{
    public class GroupedResultDto
    {
        public string SearchPhrase { get; set; }
        public string SearchSite { get; set; }
        public string MatchingUrl { get; set; }
        public List<int> ResultRank { get; set; }
        public DateTimeOffset? SearchTimeStamp { get; set; }
    }
}
