using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infoTrack.ScrapeJob.Models
{
    public class JobSettings
    {
        public int IntevalInSeconds { get; set; }
        public string SearchPhrase { get ; set; }
        public string MatchingUrl { get; set; }
        public string ApiUrl { get; set; }
        public int ApiPort { get; set; }
    }
}
