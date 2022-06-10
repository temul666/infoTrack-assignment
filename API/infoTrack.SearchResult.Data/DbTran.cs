using infoTrack.SearchResult.Core.Repositories;
using infoTrack.SearchResult.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infoTrack.SearchResult.Data
{
    public class DbTran: IDbTran
    {
        private readonly ResultLogDbContext _context;
        private ResultLogRepository _repository;

        public DbTran(ResultLogDbContext context)
        {
            _context = context;
        }

        IResultLogRepository IDbTran.ResultLogs => _repository ??= new ResultLogRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
