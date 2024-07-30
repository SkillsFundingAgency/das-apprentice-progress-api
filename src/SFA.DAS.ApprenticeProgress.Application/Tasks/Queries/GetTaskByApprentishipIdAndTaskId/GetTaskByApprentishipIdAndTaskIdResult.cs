using System.Collections.Generic;
using SFA.DAS.ApprenticeProgress.Application.Models;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetTaskByApprentishipIdAndTaskIdResult : QueryResult<GetTaskByApprentishipIdAndTaskIdResult>
    {
        public List<Domain.Entities.Task> Tasks { get; set; }
    }
}
