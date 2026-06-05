using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeProgress.Data;
using SFA.DAS.ApprenticeProgress.Domain.Entities;

namespace SFA.DAS.ApprenticeProgress.Application.Commands
{
    public class AddOrUpdateKsbProgressCommandHandler : IRequestHandler<AddOrUpdateKsbProgressCommand, Unit>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;

        public AddOrUpdateKsbProgressCommandHandler(ApprenticeProgressDataContext ApprenticeProgressDataContext)
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }

        public async Task<Unit> Handle(AddOrUpdateKsbProgressCommand request, CancellationToken cancellationToken)
        {
            var ksbProgress = await _ApprenticeProgressDataContext.KSBProgress
                .Where(x =>
                    x.KSBId == request.KSBId &&
                    x.ApprenticeshipId == request.ApprenticeshipId)
                .SingleOrDefaultAsync(cancellationToken);

            if (ksbProgress != null)
            {
                // Capture old status to detect change
                var oldStatus = (int)ksbProgress.CurrentStatus;

                // Update fields
                ksbProgress.CurrentStatus = (Domain.Entities.KSBStatus)request.CurrentStatus;
                ksbProgress.Note = request.Note;

                // If status actually changed, add a history record
                if (oldStatus != request.CurrentStatus)
                {
                    // Ensure KSBProgressId is not null and cast to long (database identity is int)
                    long progressId = ksbProgress.KSBProgressId.Value;
                    var history = new KSBProgressStatusHistory
                    {
                        KSBProgressId = progressId,
                        Status = request.CurrentStatus,
                        StatusTime = DateTime.UtcNow
                    };
                    _ApprenticeProgressDataContext.KSBProgressStatusHistory.Add(history);
                }

                await _ApprenticeProgressDataContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                // Insert new record – no history logged (per requirement)
                var newKsbProgress = new Domain.Entities.KSBProgress
                {
                    ApprenticeshipId = request.ApprenticeshipId,
                    KSBProgressType = (Domain.Entities.KSBProgressType)request.KSBProgressType,
                    KSBId = request.KSBId,
                    KSBKey = request.KsbKey,
                    CurrentStatus = (Domain.Entities.KSBStatus)request.CurrentStatus,
                    Note = request.Note
                };

                _ApprenticeProgressDataContext.Add(newKsbProgress);
                await _ApprenticeProgressDataContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
