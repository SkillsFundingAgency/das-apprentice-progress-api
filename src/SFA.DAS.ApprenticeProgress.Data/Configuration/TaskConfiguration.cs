using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.ApprenticeProgress.Domain.Entities;

namespace SFA.DAS.ApprenticeProgress.Data.Configuration
{
    [ExcludeFromCodeCoverage]
    public class TaskConfiguration : IEntityTypeConfiguration<Domain.Entities.Task>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
        {
            builder.ToTable("Task");

            builder.HasKey(x => x.TaskId);

            builder.HasMany(t => t.TaskFiles)
                .WithOne()
                .HasForeignKey(f => f.TaskId);

            builder.HasMany(t => t.TaskReminders)
                .WithOne()
                .HasForeignKey(f => f.TaskId);      

            builder.HasMany(t => t.TaskLinkedKsbs)
                .WithOne()
                .HasForeignKey(f => f.TaskId);                      

            builder.HasIndex(x => x.ApprenticeshipId).IsUnique();
        }
    }
}
