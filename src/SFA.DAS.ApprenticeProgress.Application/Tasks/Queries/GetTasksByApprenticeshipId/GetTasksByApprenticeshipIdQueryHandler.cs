using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

using SFA.DAS.ApprenticeProgress.Data;
using SFA.DAS.ApprenticeProgress.Domain.Entities;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetTasksByApprenticeshipIdQueryHandler : IRequestHandler<GetTasksByApprenticeshipIdQuery, GetTasksByApprenticeshipIdResult>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;

        public GetTasksByApprenticeshipIdQueryHandler(ApprenticeProgressDataContext ApprenticeProgressDataContext)
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }
        
        public async Task<GetTasksByApprenticeshipIdResult> Handle(GetTasksByApprenticeshipIdQuery request, CancellationToken cancellationToken)
        {
            var status = (Domain.Entities.Task.TaskStatus)request.Status;
        
            var query = _ApprenticeProgressDataContext.Task
                .Where(x => x.ApprenticeshipId == request.ApprenticeshipId && x.Status == status)
                .AsNoTracking();

            query = status switch {
                Domain.Entities.Task.TaskStatus.Todo => 
                    query.Where(x => x.DueDate >= request.FromDate && x.DueDate <= request.ToDate),
                Domain.Entities.Task.TaskStatus.Done => 
                    query.Where(x => x.CompletionDateTime >= request.FromDate && x.CompletionDateTime <= request.ToDate),
                _ => query
            };

            // Get tasks with relationships
            var tasks = await query
                .Include(t => t.TaskFiles)
                .Include(t => t.TaskReminders)
                .Include(t => t.TaskLinkedKsbs)
                .AsSplitQuery()
                .ToListAsync(cancellationToken);

            // Optimized category handling
            if (tasks.Count > 0)
            {
                // Get distinct category IDs from retrieved tasks
                var categoryIds = tasks
                    .Select(t => t.ApprenticeshipCategoryId)
                    .Distinct()
                    .ToList();

                // Batch fetch required categories
                var categoryDict = await _ApprenticeProgressDataContext.ApprenticeshipCategory
                    .Where(x => categoryIds.Contains(x.CategoryId))
                    .AsNoTracking()
                    .ToDictionaryAsync(x => x.CategoryId, cancellationToken);

                // Map categories in memory
                foreach (var task in tasks)
                {
                    task.ApprenticeshipCategory = categoryDict.TryGetValue((int)task.ApprenticeshipCategoryId, out var category) 
                        ? new List<ApprenticeshipCategory> { category } 
                        : new List<ApprenticeshipCategory>();
                }
            }

            return new GetTasksByApprenticeshipIdResult { Tasks = tasks };
        }
    }
}
