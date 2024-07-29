using System;

namespace SFA.DAS.ApprenticeProgress.Application.Models
{
    public class Task
    {
        public int ApprenticeshipProgressId { get; set; }
        public int?  TaskId { get; set; }
        public DateTime?  DueDate { get; set; }
        public int?  ReminderValueBefore { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Note { get; set; }
        public DateTime?  CompletionDateTime { get; set; }
        public DateTime? CreatedDateTime { get; set; }
    }
}