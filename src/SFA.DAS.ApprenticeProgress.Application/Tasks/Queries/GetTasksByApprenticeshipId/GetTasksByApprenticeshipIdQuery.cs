using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetTasksByApprenticeshipIdQuery : IRequest<GetTasksByApprenticeshipIdResult>
    {
        public Guid ApprenticeshipId { get; set; }
        public int Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}