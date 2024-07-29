using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.ApprenticeProgress.Data.Configuration
{
    public class TaskFile : IEntityTypeConfiguration<Domain.Entities.TaskFile>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.TaskFile> builder)
        {
            builder.ToTable("TaskFile");
            builder.HasKey(x=>x.TaskFileId);
            builder.Property(x => x.TaskFileId).HasColumnName("TaskFileId").HasColumnType("int").IsRequired();
            builder.HasIndex(x => x.TaskFileId).IsUnique();
        }
    }
}
