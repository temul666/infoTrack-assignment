using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infoTrack.ScrapeJob.Worker
{
    public interface ISearchWorker
    {
        Task DoWorkAsync(string searchPhrase, string matchingUrl);
    }
}
