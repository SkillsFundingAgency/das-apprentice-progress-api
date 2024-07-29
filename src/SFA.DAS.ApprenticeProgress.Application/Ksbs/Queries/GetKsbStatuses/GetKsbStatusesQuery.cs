using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetKsbStatusesQuery : IRequest<GetKsbStatusesResult>
    {
        public Guid ApprenticeshipId { get; set; }
    }
}