using System;

namespace SFA.DAS.ApprenticeProgress.Domain.Entities
{
    public class TaskReminder
    {
        public int TaskId { get; set; }
        public int? ReminderId { get; set; }
        public int? ReminderValue { get; set; }
        public ReminderUnit? ReminderUnit { get; set; }
        public ReminderStatus? Status { get; set; }
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