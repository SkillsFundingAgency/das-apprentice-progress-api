using System;

namespace SFA.DAS.ApprenticeProgress.Domain.Entities
{
    public class KSBProgress
    {
        public Guid ApprenticeshipId { get; set; }
        public int? KSBProgressId { get; set; }
        public KSBProgressType? KSBProgressType { get; set; }
        public Guid? KSBId { get; set; }
        public string KSBKey { get; set; }
        public KSBStatus? CurrentStatus { get; set; }
    }

    [Flags]
    public enum KSBProgressType
    {
        Knowledge = 0,
        Skill = 1,
        Behaviour = 2
    }

    [Flags]
    public enum KSBStatus
    {
        NotStarted = 0,
        InProgress = 1,
        ReadyForReview = 2,
        Completed = 3
    }
}