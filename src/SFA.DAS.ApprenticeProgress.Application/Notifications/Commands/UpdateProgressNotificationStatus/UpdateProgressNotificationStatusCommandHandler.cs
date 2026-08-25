using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Notifications.Commands.UpdateProgressNotificationStatus
{
    public class UpdateProgressNotificationStatusCommandHandler : IRequestHandler<UpdateProgressNotificationStatusCommand, Unit>
    {
        private readonly ApprenticeProgressDataContext _apprenticeProgressDataContext;
        public UpdateProgressNotificationStatusCommandHandler(ApprenticeProgressDataContext apprenticeProgressDataContext)
        {
            _apprenticeProgressDataContext = apprenticeProgressDataContext;
        }
        public async Task<Unit> Handle(UpdateProgressNotificationStatusCommand request, CancellationToken cancellationToken)
        {
            var notification = await _apprenticeProgressDataContext.ApprenticeshipProgressNotification
                .Where(x => x.NotificationId == request.NotificationId && x.ApprenticeProgressId == request.ApprenticeProgressId)
                .SingleOrDefaultAsync(cancellationToken);

            if (notification != null) 
            {
                notification.IsEnabled = false;

                await _apprenticeProgressDataContext.SaveChangesAsync(cancellationToken);
            }            

            return Unit.Value;
        }
    }
}
