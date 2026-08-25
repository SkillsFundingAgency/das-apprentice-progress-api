using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Notifications.Queries.GetApprenticeshipProgressNotifications
{
    public class GetApprenticeshipProgressNotificationsByIdQueryHandler : IRequestHandler<GetApprenticeshipProgressNotificationsByIdQuery, GetApprenticeshipProgressNotificationsByIdResult>       
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;

        public GetApprenticeshipProgressNotificationsByIdQueryHandler(ApprenticeProgressDataContext ApprenticeProgressDataContext)
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }

        public async Task<GetApprenticeshipProgressNotificationsByIdResult> Handle(GetApprenticeshipProgressNotificationsByIdQuery request, CancellationToken cancellationToken)
        {
            var apprenticeshipProgress = await _ApprenticeProgressDataContext.ApprenticeshipProgress
                .Where(apprenticeshipProgress => apprenticeshipProgress.ApprenticeAccountId == request.ApprenticeIdentifier)
                .FirstOrDefaultAsync(cancellationToken);

            var apprenticeshipProgressNotifications = await _ApprenticeProgressDataContext.ApprenticeshipProgressNotification
                .Where(notification => notification.ApprenticeProgressId == apprenticeshipProgress.Id)
                .ToListAsync();

            var result = new GetApprenticeshipProgressNotificationsByIdResult
            {
                ApprenticeshipProgress = apprenticeshipProgress,
                ApprenticeshipProgressNotification = apprenticeshipProgressNotifications
            };
            
            return result;
        }
    }
}
