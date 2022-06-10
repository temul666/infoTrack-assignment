using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infoTrack.ScrapeJob.Models
{
    public class MatchingResult
    {
        public string SearchPhrase { get; set; }
        public string SearchSite { get; set; }
        public string MatchingUrl { get; set; }
        public int ResultRank { get; set; }
        public DateTimeOffset SearchTimeStamp { get; set; }
    }
}
