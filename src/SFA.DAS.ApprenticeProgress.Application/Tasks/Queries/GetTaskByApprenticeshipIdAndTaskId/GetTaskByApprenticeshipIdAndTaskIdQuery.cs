using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetTaskByApprenticeshipIdAndTaskIdQuery : IRequest<GetTaskByApprenticeshipIdAndTaskIdResult>
    {
        public long ApprenticeshipId { get; set; }
        public int TaskId { get; set; }
    }
}