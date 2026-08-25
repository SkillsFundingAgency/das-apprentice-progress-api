using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Notifications.Queries.GetProgressNotificationsById
{
    public class GetProgressNotificationsByIdQueryHandler : IRequestHandler<GetProgressNotificationsByIdQuery, GetProgressNotificationsByIdResult>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;

        public GetProgressNotificationsByIdQueryHandler(ApprenticeProgressDataContext apprenticeProgressDataContext)
        {
            _ApprenticeProgressDataContext = apprenticeProgressDataContext;
        }

        public async Task<GetProgressNotificationsByIdResult> Handle(GetProgressNotificationsByIdQuery request, CancellationToken cancellationToken)
        {
            var notification = await _ApprenticeProgressDataContext.ProgressNotification
                .Where(noti => noti.Id == request.NotificationId)
                .FirstOrDefaultAsync();

            var result = new GetProgressNotificationsByIdResult
            {
                ProgressNotification = notification
            };

            return result;
        }

    }
}
