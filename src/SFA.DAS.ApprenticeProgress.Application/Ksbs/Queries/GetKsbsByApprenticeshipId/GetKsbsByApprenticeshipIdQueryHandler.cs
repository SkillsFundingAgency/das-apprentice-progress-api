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
                .Where(x => x.ApprenticeshipId == request.ApprenticeshipId)
                .ToListAsync(cancellationToken);

            if (ksbs.Count == 0)
                return new GetKsbsByApprenticeshipIdResult { KSBProgresses = ksbs };

            var ksbIds = ksbs.Select(k => k.KSBProgressId).ToList();

            // Get all TaskKSBs in one query
            var taskJoins = await _ApprenticeProgressDataContext.TaskKSBs
                .Where(x => ksbIds.Contains(x.KSBProgressId))
                .Select(x => new { x.KSBProgressId, x.TaskId })
                .ToListAsync(cancellationToken);

            // Get all relevant tasks in one query
            var taskIds = taskJoins.Select(t => t.TaskId).Distinct().ToList();
            var tasks = await _ApprenticeProgressDataContext.Task
                .Where(x => taskIds.Contains(x.TaskId))
                .ToDictionaryAsync(x => x.TaskId, x => x, cancellationToken);

            // Create lookup dictionary for KSBProgressId -> TaskIds
            var ksbTaskMapping = taskJoins
                .GroupBy(x => x.KSBProgressId)
                .ToDictionary(g => g.Key, g => g.Select(x => x.TaskId).ToList());

            // Map tasks to KSBs using in-memory operations
            foreach (var ksb in ksbs)
            {
                if (ksbTaskMapping.TryGetValue(ksb.KSBProgressId, out var ids))
                {
                    ksb.Tasks = ids.Select(id => tasks.TryGetValue(id, out var task) ? task : null)
                        .Where(t => t != null)
                        .ToList();
                }
            }

            return new GetKsbsByApprenticeshipIdResult { KSBProgresses = ksbs };
        }
    }
}
