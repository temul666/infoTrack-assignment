using infoTrack.SearchResult.Core.Domain;

namespace infoTrack.SearchResult.Services.Services.Interface
{
    public interface IResultLogService
    {
        Task<IEnumerable<ResultLog>> GetSearchResults(int skip, int take);
        Task<IEnumerable<ResultLog>> AddResults(IEnumerable<ResultLog> result);

    }
}
