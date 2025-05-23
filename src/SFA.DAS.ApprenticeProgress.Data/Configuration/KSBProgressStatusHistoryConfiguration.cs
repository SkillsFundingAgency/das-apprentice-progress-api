﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.ApprenticeProgress.Data.Configuration
{
    [ExcludeFromCodeCoverage]
    public class KSBProgressStatusHistoryConfiguration : IEntityTypeConfiguration<Domain.Entities.KSBProgressStatusHistory>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.KSBProgressStatusHistory> builder)
        {
            builder.ToTable("KSBProgressStatusHistory");
            builder.HasKey(x=>x.KSBProgressId);
            builder.Property(x => x.Status).HasColumnName("Status").HasColumnType("int").IsRequired();
            builder.HasIndex(x => x.KSBProgressId).IsUnique();
        }
    }
}
