﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.ApprenticeProgress.Domain.Entities
{
    public class KSBProgress
    {
        public int? KSBProgressId { get; set; }
        public long ApprenticeshipId { get; set; }
        public KSBProgressType? KSBProgressType { get; set; }
        public Guid KSBId { get; set; }
        public string KSBKey { get; set; }
        public KSBStatus? CurrentStatus { get; set; }
        public string Note { get; set; }

        [NotMapped]
        public List<Task> Tasks { get; set; }
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
