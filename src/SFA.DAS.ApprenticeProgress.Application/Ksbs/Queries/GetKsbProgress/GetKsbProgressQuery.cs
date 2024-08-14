using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetKsbProgressQuery : IRequest<GetKsbProgressResult>
    {
        public long ApprenticeshipId { get; set; }
    }
}