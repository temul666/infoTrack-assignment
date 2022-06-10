using infoTrack.SearchResult.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infoTrack.SearchResult.Core.Repositories
{
    public interface IResultLogRepository: IRepository<ResultLog>
    {
        Task<IEnumerable<ResultLog>> GetAllResultLogsAsync(int skip, int take);
        Task AddResultLogsAsync(IEnumerable<ResultLog> results);
    }
}
