using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Notifications.Queries.GetProgressNotificationsById
{
    public class GetProgressNotificationsByIdQuery : IRequest<GetProgressNotificationsByIdResult>
    {
        public Guid NotificationId { get; set; }
    }
}
