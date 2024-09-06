using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetKsbsByApprenticeshipIdQueryHandler : IRequestHandler<GetKsbsByApprenticeshipIdQuery, GetKsbsByApprenticeshipIdResult>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;

        public GetKsbsByApprenticeshipIdQueryHandler(ApprenticeProgressDataContext ApprenticeProgressDataContext)
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }

        public async Task<GetKsbsByApprenticeshipIdResult> Handle(GetKsbsByApprenticeshipIdQuery request, CancellationToken cancellationToken)
        {
            var ksbs = await _ApprenticeProgressDataContext.KSBProgress
                .Where(x =>
                    x.ApprenticeshipId == request.ApprenticeshipId)
                .ToListAsync(cancellationToken);

            if (ksbs.Count > 0)
            {
                foreach (var ksb in ksbs)
                {
                    var taskJoins = await _ApprenticeProgressDataContext.TaskKSBs
                        .Where(x =>
                            x.KSBProgressId == ksb.KSBProgressId)
                            .Select(y => y.TaskId)
                        .ToListAsync(cancellationToken);

                    if (taskJoins != null)
                    {
                        var tasks = await _ApprenticeProgressDataContext.Task
                            .Where(x => taskJoins.Contains(x.TaskId))
                            .ToListAsync(cancellationToken);

                        ksb.Tasks = tasks;
                    }

                }
            }

            var result = new GetKsbsByApprenticeshipIdResult
            {
                KSBProgresses = ksbs
            };

            return result;
        }
    }
}