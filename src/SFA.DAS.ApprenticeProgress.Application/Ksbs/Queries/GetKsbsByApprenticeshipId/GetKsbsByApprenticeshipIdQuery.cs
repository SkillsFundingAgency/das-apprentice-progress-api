using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetKsbsByApprenticeshipIdQuery : IRequest<GetKsbsByApprenticeshipIdResult>
    {
        public long ApprenticeshipId { get; set; }
    }
}