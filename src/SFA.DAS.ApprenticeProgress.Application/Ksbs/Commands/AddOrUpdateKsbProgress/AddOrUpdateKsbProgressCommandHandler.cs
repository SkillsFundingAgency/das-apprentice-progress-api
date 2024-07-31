using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Commands

{
    public class AddOrUpdateKsbProgressCommandHandler : IRequestHandler<AddOrUpdateKsbProgressCommand, Unit>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;

        public AddOrUpdateKsbProgressCommandHandler
        (
            ApprenticeProgressDataContext ApprenticeProgressDataContext
        )
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }

        public async Task<Unit> Handle(AddOrUpdateKsbProgressCommand request, CancellationToken cancellationToken)
        {
            // get ksbprogress first
            var ksbProgress = await _ApprenticeProgressDataContext.KSBProgress
                .Where(x =>
                    x.KSBId == request.KSBId
                    &&
                    x.ApprenticeshipId == request.ApprenticeshipId
                    )
                .SingleOrDefaultAsync();

            if (ksbProgress != null)
            {
                // update it
                ksbProgress.CurrentStatus = (Domain.Entities.KSBStatus)request.CurrentStatus;
                ksbProgress.Note = request.Note;
                _ApprenticeProgressDataContext.SaveChanges();
            }
            else
            {
                // add one
                var ksbprogress = new Domain.Entities.KSBProgress
                {
                    ApprenticeshipId = request.ApprenticeshipId,
                    KSBProgressType = (Domain.Entities.KSBProgressType)request.KSBProgressType,
                    KSBId = request.KSBId,
                    KSBKey = request.KsbKey,
                    CurrentStatus = (Domain.Entities.KSBStatus)request.CurrentStatus,
                    Note = request.Note
                };

                _ApprenticeProgressDataContext.Add(ksbprogress);
                _ApprenticeProgressDataContext.SaveChanges();
            }

            return await Task.FromResult(Unit.Value);
        }
    }
}