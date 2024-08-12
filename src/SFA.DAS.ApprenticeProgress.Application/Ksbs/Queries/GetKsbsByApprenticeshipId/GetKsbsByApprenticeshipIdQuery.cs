using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetKsbsByApprenticeshipIdQuery : IRequest<GetKsbsByApprenticeshipIdResult>
    {
        public Guid ApprenticeshipId { get; set; }
    }
}