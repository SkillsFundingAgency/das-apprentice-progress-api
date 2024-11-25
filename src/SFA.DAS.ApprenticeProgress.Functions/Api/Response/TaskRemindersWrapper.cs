using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.ApprenticeProgress.Functions.Api.Response
{
    [ExcludeFromCodeCoverage]
    public class TaskRemindersWrapper
    {
        public List<TaskReminderModel> TaskReminders { get; set; } = new List<TaskReminderModel>();
    }
}