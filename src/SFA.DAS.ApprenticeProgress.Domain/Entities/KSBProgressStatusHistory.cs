using System;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.ApprenticeProgress.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class KSBProgressStatusHistory
    {
        public int KSBProgressStatusHistoryId { get; set; }
        public long KSBProgressId { get; set; }
        public int? Status { get; set; }
        public DateTime? StatusTime { get; set; }
    }  
}
