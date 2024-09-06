using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Commands
{
    public class UpdateKsbCommand : IRequest<Unit>
    {
        public Guid ApprenticeId { get; set; }
        public int KsbKey { get; set; }
    }
}