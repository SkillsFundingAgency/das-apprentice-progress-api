using System;

namespace SFA.DAS.ApprenticeProgress.Application.Models
{
    public class KSBProgress
    {
        public int ApprenticeshipProgressId { get; set; }
        public int? KSBProgressId { get; set; }
        public int? KSBProgressType { get; set; }
        public int? KSBId { get; set; }
        public int? KSBKey { get; set; }
        public int? CurrentStatus { get; set; }
    }  
}