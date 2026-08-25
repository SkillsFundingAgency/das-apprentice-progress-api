using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeProgress.Data;

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

            var result = new GetProgressNotificationsToCheckResult
            {
                Notifications = notificationsToCheck
            };

            return result;
        }
    }
}
