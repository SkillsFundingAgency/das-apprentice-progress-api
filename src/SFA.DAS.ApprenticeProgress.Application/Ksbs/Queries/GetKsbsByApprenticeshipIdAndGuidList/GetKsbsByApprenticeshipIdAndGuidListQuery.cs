using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetKsbsByApprenticeshipIdAndGuidListQuery : IRequest<GetKsbsByApprenticeshipIdAndGuidListResult>
    {
        public long ApprenticeshipId { get; set; }
        public Guid[] KsbIds { get; set; }
    }
}