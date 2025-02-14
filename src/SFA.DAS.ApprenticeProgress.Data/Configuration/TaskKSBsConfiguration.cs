using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.ApprenticeProgress.Data.Configuration
{
    [ExcludeFromCodeCoverage]
    public class TaskKSBsConfiguration : IEntityTypeConfiguration<Domain.Entities.TaskKSBs>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.TaskKSBs> builder)
        {
            builder.ToTable("TaskKSBs");
            builder.HasKey(vf => new { vf.TaskId, vf.KSBProgressId });
            builder.Property(x => x.KSBProgressId).HasColumnName("KSBProgressId").HasColumnType("int").IsRequired();
        }
    }
}
