using System;

namespace SFA.DAS.ApprenticeProgress.Application.Models
{
    public class KSBProgressStatusHistory
    {
        public int KSBProgressId { get; set; }
        public int? Status { get; set; }
        public int? StatusTime { get; set; }
    }  
}