using System;

namespace SFA.DAS.ApprenticeProgress.Application.Models
{
    public class TaskReminder
    {
        public int TaskId  { get; set; }
        public int? ReminderId  { get; set; }
        public int? ReminderValue  { get; set; }
        public int? ReminderUnit  { get; set; }
        public int? Status  { get; set; }
    }
}