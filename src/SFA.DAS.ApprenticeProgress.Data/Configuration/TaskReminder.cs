using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.ApprenticeProgress.Data.Configuration
{
    [ExcludeFromCodeCoverage]
    public class TaskReminder : IEntityTypeConfiguration<Domain.Entities.TaskReminder>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.TaskReminder> builder)
        {
            builder.ToTable("TaskReminder");
            builder.HasKey(x=>x.ReminderId);
            builder.Property(x => x.ReminderId).HasColumnName("ReminderId").HasColumnType("int").IsRequired();
            builder.HasIndex(x => x.ReminderId).IsUnique();
        }
    }
}
