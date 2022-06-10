using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infoTrack.SearchResult.Core.Repositories
{
    public interface IDbTran : IDisposable
    {
        IResultLogRepository  ResultLogs { get; }
        Task<int> CommitAsync();
    }
}
