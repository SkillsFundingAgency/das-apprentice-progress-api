using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.ApprenticeProgress.Data.Configuration
{
    public class TaskKSBs : IEntityTypeConfiguration<Domain.Entities.TaskKSBs>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.TaskKSBs> builder)
        {
            builder.ToTable("TaskKSBs");
            builder.HasKey(vf => new { vf.TaskId, vf.KSBProgressId });
            builder.Property(x => x.KSBProgressId).HasColumnName("KSBProgressId").HasColumnType("int").IsRequired();
        }
    }
}
