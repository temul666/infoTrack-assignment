using infoTrack.SearchResult.Core.Domain;
using infoTrack.SearchResult.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace infoTrack.SearchResult.Data
{
    public class ResultLogDbContext: DbContext
    {
        public DbSet<ResultLog> ResultLogs { get; set; }

        public ResultLogDbContext(DbContextOptions<ResultLogDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new ResultLogConfiguration());

        }
    }
}
