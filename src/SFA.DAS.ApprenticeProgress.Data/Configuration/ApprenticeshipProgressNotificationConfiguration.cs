using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.ApprenticeProgress.Data.Configuration
{
    public class ApprenticeshipProgressNotificationConfiguration : IEntityTypeConfiguration<Domain.Entities.ApprenticeshipProgressNotification>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ApprenticeshipProgressNotification> builder)
        {
            builder.ToTable("ApprenticeshipProgressNotification");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.ApprenticeProgressId)
                .HasColumnName("ApprenticeProgressId")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.NotificationId)
                .HasColumnName("NotificationId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.IsEnabled)
                .HasColumnName("IsEnabled")
                .HasColumnType("bit")
                .IsRequired();

            builder.HasOne(x => x.ApprenticeshipProgress)
                .WithMany()
                .HasForeignKey(x => x.ApprenticeProgressId)
                .HasPrincipalKey(x => x.Id);

            builder.HasOne(x => x.ProgressNotification)
                .WithMany()
                .HasForeignKey(x => x.NotificationId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
