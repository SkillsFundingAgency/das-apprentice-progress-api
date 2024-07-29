using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetTaskByApprentishipIdAndTaskIdQuery : IRequest<GetTaskByApprentishipIdAndTaskIdResult>
    {
        public Guid ApprenticeshipId { get; set; }
        public int TaskId { get; set; }
    }
}