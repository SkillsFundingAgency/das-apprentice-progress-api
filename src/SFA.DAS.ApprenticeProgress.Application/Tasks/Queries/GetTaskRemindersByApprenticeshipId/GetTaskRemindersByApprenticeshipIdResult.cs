using System.Collections.Generic;
using SFA.DAS.ApprenticeProgress.Application.Models;

namespace SFA.DAS.ApprenticeProgress.Application.Tasks.Queries
{
    public class GetTaskRemindersByApprenticeshipIdResult : QueryResult<GetTaskRemindersByApprenticeshipIdResult>
    {
        public List<TaskReminderModel> TaskReminders { get; set; }
    }
}
