using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Notifications.Queries.GetApprenticeshipProgressNotifications
{
    public class GetApprenticeshipProgressNotificationsByIdQuery : IRequest<GetApprenticeshipProgressNotificationsByIdResult>
    {
        public Guid ApprenticeIdentifier { get; set; }
    }
}
