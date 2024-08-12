using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetKsbProgressQuery : IRequest<GetKsbProgressResult>
    {
        public Guid ApprenticeshipId { get; set; }
    }
}