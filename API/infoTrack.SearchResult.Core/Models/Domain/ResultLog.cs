namespace infoTrack.SearchResult.Core.Domain
{
    public class ResultLog
    {
        public int Id { get; set; }
        public string SearchPhrase { get; set; }
        public string SearchSite { get; set; }
        public string MatchingUrl { get; set; }
        public int ResultRank { get; set; }
        public DateTimeOffset? SearchTimeStamp { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
    }
}
