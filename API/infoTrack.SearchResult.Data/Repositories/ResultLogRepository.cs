using infoTrack.SearchResult.Core.Domain;
using infoTrack.SearchResult.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace infoTrack.SearchResult.Data.Repositories
{
    public class ResultLogRepository : Repository<ResultLog>, IResultLogRepository
    {
        public ResultLogRepository(ResultLogDbContext context)
            : base(context)
        { }

        public async Task AddResultLogsAsync(IEnumerable<ResultLog> results)
        {
            await ResultLogDbContext.ResultLogs.AddRangeAsync(results);
        }

        public async Task<IEnumerable<ResultLog>> GetAllResultLogsAsync(int skip, int take)
        {
            return await ResultLogDbContext.ResultLogs.OrderBy(x => x.Id).Skip(skip).Take(take).ToListAsync();
        }

        private ResultLogDbContext ResultLogDbContext
        {
            get { return (ResultLogDbContext)Context; }
        }
    }
}
