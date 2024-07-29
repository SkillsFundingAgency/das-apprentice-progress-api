using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetKsbTypesQuery : IRequest<GetKsbTypesResult>
    {
        public Guid ApprenticeshipId { get; set; }
    }
}