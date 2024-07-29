using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class RemoveTaskByApprentishipIdAndTaskIdCommand : IRequest<Unit>
    {
        public Guid ApprenticeshipId { get; set; }
        public int TaskId { get; set; }
    }
}