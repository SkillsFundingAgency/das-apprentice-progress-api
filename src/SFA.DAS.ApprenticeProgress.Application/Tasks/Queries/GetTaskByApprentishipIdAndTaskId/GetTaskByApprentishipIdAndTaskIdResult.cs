using SFA.DAS.ApprenticeProgress.Application.Models;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetTaskByApprentishipIdAndTaskIdResult : QueryResult<GetTaskByApprentishipIdAndTaskIdResult>
    {
        public Domain.Entities.Task Task { get; set; }
    }
}
