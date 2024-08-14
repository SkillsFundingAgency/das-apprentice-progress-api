using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class RemoveTaskByApprenticeshipIdAndTaskIdCommand : IRequest<Unit>
    {
        public long ApprenticeshipId { get; set; }
        public int TaskId { get; set; }
    }
}