using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using infoTrack.ScrapeJob.Models;

namespace infoTrack.ScrapeJob.Services
{
    public interface IScrapeService
    {
        Task<IEnumerable<MatchingResult>> SearchAndScrapeAsync(string searchPhrase, string matchingUrl);
    }
}
