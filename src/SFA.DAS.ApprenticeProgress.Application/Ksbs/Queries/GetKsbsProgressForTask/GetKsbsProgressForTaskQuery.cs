using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetKsbsProgressForTaskQuery : IRequest<GetKsbsProgressForTaskResult>
    {
        public long ApprenticeshipId { get; set; }
        public int TaskId { get; set; }
    }
}