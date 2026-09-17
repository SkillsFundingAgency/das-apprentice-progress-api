using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Notifications.Commands.AddApprenticeshipProgress
{
    public class AddApprenticeshipProgressCommandHandler : IRequestHandler<AddApprenticeshipProgressCommand, Unit>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;

        public AddApprenticeshipProgressCommandHandler (ApprenticeProgressDataContext ApprenticeProgressDataContext)
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }

        public async Task<Unit> Handle(AddApprenticeshipProgressCommand request, CancellationToken cancellationToken)
        {
            var apprenticeshipProgress = new Domain.Entities.ApprenticeshipProgress
            {
                ApprenticeAccountId = request.ApprenticeIdentifier,
                FirstLoggedIn = DateTime.Now,
                IsEnabled = true,
            };

            await using var transaction = await _ApprenticeProgressDataContext.Database.BeginTransactionAsync(cancellationToken);

            _ApprenticeProgressDataContext.Add(apprenticeshipProgress);
            await _ApprenticeProgressDataContext.SaveChangesAsync(cancellationToken);

            var notifications = await _ApprenticeProgressDataContext.ProgressNotification.Where(n => n.IsEnabled).ToListAsync(cancellationToken);

            foreach(var notification in notifications)
            {
                _ApprenticeProgressDataContext.Add(new Domain.Entities.ApprenticeshipProgressNotification
                {                    
                    ApprenticeProgressId = apprenticeshipProgress.Id,
                    NotificationId = notification.Id,
                    IsEnabled = true
                });
            }

            await _ApprenticeProgressDataContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
