using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.ApprenticeProgress.Data.Configuration
{
    public class TaskCategory : IEntityTypeConfiguration<Domain.Entities.TaskCategory>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.TaskCategory> builder)
        {
            builder.ToTable("TaskCategory");
            builder.HasKey(vf => new { vf.TaskId, vf.CategoryId });
            builder.Property(x => x.CategoryId).HasColumnName("CategoryId").HasColumnType("int").IsRequired();
        }
    }
}
