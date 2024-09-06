using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.ApprenticeProgress.Data.Configuration
{
    [ExcludeFromCodeCoverage]
    public class Task : IEntityTypeConfiguration<Domain.Entities.Task>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
        {
            builder.ToTable("Task");
            builder.HasKey(x=>x.TaskId);
            builder.HasIndex(x => x.ApprenticeshipId).IsUnique();
        }
    }
}
