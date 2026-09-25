using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Notifications.Commands.AddApprenticeshipProgress
{
    public class AddApprenticeshipProgressCommand : IRequest<Unit>
    {
        public Guid ApprenticeIdentifier { get; set; }
    }
}
