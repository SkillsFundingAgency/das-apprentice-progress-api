using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetKsbsByApprenticeshipIdAndGuidListQuery : IRequest<GetKsbsByApprenticeshipIdAndGuidListResult>
    {
        public Guid ApprenticeshipId { get; set; }
        public Guid[] KsbIds { get; set; }
    }
}