using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.ApprenticeProgress.Data.Configuration
{
    [ExcludeFromCodeCoverage]
    public class KSBProgressConfiguration : IEntityTypeConfiguration<Domain.Entities.KSBProgress>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.KSBProgress> builder)
        {
            builder.ToTable("KSBProgress");
            builder.HasKey(x=>x.KSBProgressId);
            builder.Property(x => x.KSBProgressId).HasColumnName("KSBProgressId").HasColumnType("int").IsRequired();
            builder.HasIndex(x => x.KSBProgressId).IsUnique();
            

        }
    }
}
