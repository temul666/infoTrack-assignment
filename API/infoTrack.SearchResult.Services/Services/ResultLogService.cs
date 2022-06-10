using infoTrack.SearchResult.Core.Domain;
using infoTrack.SearchResult.Core.Repositories;
using infoTrack.SearchResult.Services.Services.Interface;

namespace infoTrack.SearchResult.Services
{
    public class ResultLogService : IResultLogService
    {
        private readonly IDbTran _dbTran;

        public ResultLogService(IDbTran dbTran)
        {
            _dbTran = dbTran;
        }

        public async Task<IEnumerable<ResultLog>> GetSearchResults(int skip, int take)
        {
            return await _dbTran.ResultLogs.GetAllResultLogsAsync(skip, take);
        }


        public async Task<IEnumerable<ResultLog>> AddResults(IEnumerable<ResultLog> items)
        {

            await _dbTran.ResultLogs.AddResultLogsAsync(items);
            await _dbTran.CommitAsync();
            return items;
        }
    }
}
