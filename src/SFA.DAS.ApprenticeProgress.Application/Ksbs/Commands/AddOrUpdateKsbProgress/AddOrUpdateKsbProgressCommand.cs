using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Commands
{
    public class AddOrUpdateKsbProgressCommand : IRequest<Unit>
    {
        public long ApprenticeshipId { get; set; }
        public int KSBProgressType { get; set; }
        public Guid KSBId { get; set; }
        public string KsbKey { get; set; }
        public int CurrentStatus { get; set; }
        public string Note { get; set; }
    }
}