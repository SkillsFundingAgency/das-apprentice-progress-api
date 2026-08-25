using Microsoft.EntityFrameworkCore;

namespace SFA.DAS.ApprenticeProgress.Data.Configuration
{
    public class ApprenticeshipProgressConfiguration : IEntityTypeConfiguration<Domain.Entities.ApprenticeshipProgress>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Entities.ApprenticeshipProgress> builder)
        {
            builder.ToTable("ApprenticeshipProgress");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("int").IsRequired();
            builder.HasIndex(x => x.Id).IsUnique();
        }

    }
}
