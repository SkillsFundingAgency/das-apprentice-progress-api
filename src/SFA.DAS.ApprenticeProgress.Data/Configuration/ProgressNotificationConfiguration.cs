using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SFA.DAS.ApprenticeProgress.Data.Configuration
{
    public class ProgressNotificationConfiguration : IEntityTypeConfiguration<Domain.Entities.ProgressNotification>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Entities.ProgressNotification> builder)
        {
            builder.ToTable("ProgressNotification");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("Guid").IsRequired();
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}
