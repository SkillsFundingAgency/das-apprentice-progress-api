using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.ApprenticeProgress.Functions.Api
{
    [ExcludeFromCodeCoverage]
    public class TaskReminder
    {
        public int TaskId { get; set; }
        public long ApprenticeshipId { get; set; }
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

    [Flags]
    public enum ReminderUnit
    {
        Minutes = 0,
        Hours = 1,
        Days = 2
    }

    [Flags]
    public enum ReminderStatus
    {
        NotSent = 0,
        Sent = 1,
        Dismissed = 2
    }
}