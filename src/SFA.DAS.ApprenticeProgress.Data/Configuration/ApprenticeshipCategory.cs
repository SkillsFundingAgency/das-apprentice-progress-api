using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.ApprenticeProgress.Data.Configuration
{
    public class ApprenticeshipCategory : IEntityTypeConfiguration<Domain.Entities.ApprenticeshipCategory>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ApprenticeshipCategory> builder)
        {
            builder.ToTable("ApprenticeshipCategory");
            builder.HasKey(x=>x.CategoryId);
            builder.Property(x => x.Title).HasColumnName("Title").HasColumnType("string").IsRequired();
            builder.HasIndex(x => x.CategoryId).IsUnique();
        }
    }
}