using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.ApprenticeProgress.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class ApprenticeshipProgress
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid ApprenticeAccountId { get; set; }
        public long? ApprenticeshipId { get; set; }
        public DateTime FirstLoggedIn { get; set; }
        public DateTime? LastLoggedIn { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
        public bool IsEnabled { get; set; }
    }
}
