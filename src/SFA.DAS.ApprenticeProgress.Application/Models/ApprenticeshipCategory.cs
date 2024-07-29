using System;

namespace SFA.DAS.ApprenticeProgress.Application.Models
{
    public class ApprenticeshipCategory
    {
        public int CategoryId { get; set; }
        public Guid? ApprenticeshipId { get; set; }
        public string Title { get; set; }
    }
}