using System.Collections.Generic;
using SFA.DAS.ApprenticeProgress.Application.Models;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetTaskRemindersResult : QueryResult<GetTaskRemindersResult>
    {
        public List<TaskReminderModel> TaskReminders  { get; set; }
    }
}