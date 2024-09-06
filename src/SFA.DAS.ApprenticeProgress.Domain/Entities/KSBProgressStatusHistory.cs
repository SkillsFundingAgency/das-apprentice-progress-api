using System;

namespace SFA.DAS.ApprenticeProgress.Domain.Entities
{
    public class KSBProgressStatusHistory
    {
        public long KSBProgressId { get; set; }
        public int? Status { get; set; }
        public DateTime? StatusTime { get; set; }
    }  
}