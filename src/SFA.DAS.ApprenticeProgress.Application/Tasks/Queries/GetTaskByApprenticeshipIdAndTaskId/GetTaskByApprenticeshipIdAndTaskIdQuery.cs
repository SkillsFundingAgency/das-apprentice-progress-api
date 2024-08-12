using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetTaskByApprenticeshipIdAndTaskIdQuery : IRequest<GetTaskByApprenticeshipIdAndTaskIdResult>
    {
        public Guid ApprenticeshipId { get; set; }
        public int TaskId { get; set; }
    }
}