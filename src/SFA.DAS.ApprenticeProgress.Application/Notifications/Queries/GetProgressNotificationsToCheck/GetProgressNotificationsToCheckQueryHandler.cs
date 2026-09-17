using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeProgress.Data;
using SFA.DAS.ApprenticeProgress.Domain.Entities;

namespace SFA.DAS.ApprenticeProgress.Application.Notifications.Queries.GetProgressNotificationsToCheck
{
    public class GetProgressNotificationsToCheckQueryHandler : IRequestHandler<GetProgressNotificationsToCheckQuery, GetProgressNotificationsToCheckResult>
    {

        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;

        public GetProgressNotificationsToCheckQueryHandler(ApprenticeProgressDataContext apprenticeProgressDataContext)
        {
            _ApprenticeProgressDataContext = apprenticeProgressDataContext;
        }

        public async Task<GetProgressNotificationsToCheckResult> Handle(GetProgressNotificationsToCheckQuery request, CancellationToken cancellationToken)
        {
            var notificationsToCheck = await _ApprenticeProgressDataContext.ApprenticeshipProgressNotification
                .Include(x => x.ApprenticeshipProgress)
                .Include(x => x.ProgressNotification)
                .Where(x => x.IsEnabled == true)
                .ToListAsync(cancellationToken);

            var now = DateTime.UtcNow;

            var notificationsDue = notificationsToCheck
                .Where(x =>
                {
                    var notification = x.ProgressNotification;
                    var progress = x.ApprenticeshipProgress;

                    DateTime? activationDate = notification.ActivationPoint switch
                    {
                        ActivationPoint.FromStart => progress.FirstLoggedIn,
                        ActivationPoint.FromEnd => progress.PlannedEndDate,
                        _ => null
                    };

                    if (activationDate == null)
                        return false;

                    var notificationDate = notification.DelayUnit switch
                    {
                        DelayUnit.Day => activationDate.Value.AddDays(int.Parse(notification.Delay)),
                        DelayUnit.Week => activationDate.Value.AddDays(int.Parse(notification.Delay) * 7),
                        DelayUnit.Month => activationDate.Value.AddMonths(int.Parse(notification.Delay)),
                        _ => throw new ArgumentOutOfRangeException(
                            nameof(notification.DelayUnit),
                            notification.DelayUnit,
                            null)
                    };

                    return notificationDate.Date <= now.Date;
                })
                .ToList();

            var result = new GetProgressNotificationsToCheckResult
            {
                Notifications = notificationsDue
            };

            return result;
        }
    }
}
