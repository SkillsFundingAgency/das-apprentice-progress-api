using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFA.DAS.ApprenticeProgress.Domain.Entities
{
    public class Task
    {
        public int TaskId { get; set; }
        public long ApprenticeshipId { get; set; }
        public DateTime? DueDate { get; set; }
        public string Title { get; set; }
        public int? ApprenticeshipCategoryId { get; set; }
        public string Note { get; set; }
        public DateTime? CompletionDateTime { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public TaskStatus? Status { get; set; }

        [Flags]
        public enum TaskStatus
        {
            Todo = 0,
            Done = 1
        }

        [NotMapped]
        public List<ApprenticeshipCategory> ApprenticeshipCategory { get; set; }

        [NotMapped]
        public List<TaskFile> TaskFiles { get; set; }

        [NotMapped]
        public List<TaskReminder> TaskReminders { get; set; }

        [NotMapped]
        public List<TaskKSBs> TaskLinkedKsbs { get; set; }
    }
}