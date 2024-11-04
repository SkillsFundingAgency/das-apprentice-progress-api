using System;
using SFA.DAS.ApprenticeProgress.Domain.Entities;

namespace SFA.DAS.ApprenticeProgress.Application.Models
{
    public class TaskReminderModel
    {
        public int TaskId { get; set; }
        public long ApprenticeshipId { get; set; }
        public Guid ApprenticeAccountId { get; set; }
        public DateTime? DueDate { get; set; }
        public string Title { get; set; }
        public int? ApprenticeshipCategoryId { get; set; }
        public string Note { get; set; }
        public DateTime? CompletionDateTime { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? ReminderValue { get; set; }
        public ReminderUnit? ReminderUnit { get; set; }
        public ReminderStatus? ReminderStatus { get; set; }
    }
}