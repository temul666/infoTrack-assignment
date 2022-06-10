using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using infoTrack.SearchResult.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace infoTrack.SearchResult.Data.Configurations
{
    public class ResultLogConfiguration: IEntityTypeConfiguration<ResultLog>
    {
        public void Configure(EntityTypeBuilder<ResultLog> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

            builder
                .Property(m => m.SearchPhrase)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(m => m.SearchSite)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(m => m.MatchingUrl)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(m => m.ResultRank)
                .IsRequired();

            builder
                .Property(m => m.SearchTimeStamp)
                .HasConversion(new DateTimeOffsetToBinaryConverter())
                .IsRequired();

            builder
                .Property(m => m.CreatedBy)
                .IsRequired();

            builder
                .Property(m => m.CreatedDate)
                .HasDefaultValue(DateTimeOffset.Now)
                .HasConversion(new DateTimeOffsetToBinaryConverter());

            builder.ToTable("ResultLog");
        }
    }
}
