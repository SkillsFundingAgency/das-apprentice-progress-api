using System;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.ApprenticeProgress.Functions.Api.Response
{
    [ExcludeFromCodeCoverage]
    public class TaskReminderModel
    {
        public int? TaskId { get; set; } = null!;
        public long? ApprenticeshipId { get; set; } = null!;
        public DateTime? DueDate { get; set; } = null!;
        public string Title { get; set; } = null!;
        public int? ApprenticeshipCategoryId { get; set; } = null!;
        public string Note { get; set; } = null!;
        public DateTime? CompletionDateTime { get; set; } = null!;
        public DateTime? CreatedDateTime { get; set; } = null!;
        public int? ReminderValue { get; set; } = null!;
        public ReminderUnit? ReminderUnit { get; set; } = null!;
        public ReminderStatus? ReminderStatus { get; set; } = null!;
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
