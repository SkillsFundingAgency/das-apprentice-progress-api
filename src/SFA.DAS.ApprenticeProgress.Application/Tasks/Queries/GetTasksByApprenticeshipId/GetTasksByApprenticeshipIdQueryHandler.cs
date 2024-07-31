using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

using SFA.DAS.ApprenticeProgress.Data;

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
            var tasks = new List<Domain.Entities.Task>();

            if ((Domain.Entities.Task.TaskStatus)(int)request.Status == Domain.Entities.Task.TaskStatus.Todo)
            {
                tasks = await _ApprenticeProgressDataContext.Task
                    .Where(x =>
                        x.ApprenticeshipId == request.ApprenticeshipId
                        &&
                        x.DueDate >= request.FromDate
                        &&
                        x.DueDate <= request.ToDate
                        &&
                        x.Status == (Domain.Entities.Task.TaskStatus)(int)request.Status)
                    .OrderBy(x => x.DueDate)
                    .AsNoTracking()
                    .AsSingleQuery()
                    .ToListAsync(cancellationToken);
            }

            if ((Domain.Entities.Task.TaskStatus)(int)request.Status == Domain.Entities.Task.TaskStatus.Done)
            {
                tasks = await _ApprenticeProgressDataContext.Task
                    .Where(x =>
                        x.ApprenticeshipId == request.ApprenticeshipId
                        &&
                        x.CompletionDateTime >= request.FromDate
                        &&
                        x.CompletionDateTime <= request.ToDate
                        &&
                        x.Status == (Domain.Entities.Task.TaskStatus)(int)request.Status)
                    .OrderBy(x => x.CompletionDateTime)
                    .AsNoTracking()
                    .AsSingleQuery()
                    .ToListAsync(cancellationToken);
            }

            foreach (var task in tasks)
            {
                // get task categories
                var taskCategories = await _ApprenticeProgressDataContext.ApprenticeshipCategory
                .Where(x =>
                    x.CategoryId == task.ApprenticeshipCategoryId)
                .AsNoTracking()
                .AsSingleQuery()
                .ToListAsync(cancellationToken);
                task.ApprenticeshipCategory = taskCategories;

                // get task files
                var taskFiles = await _ApprenticeProgressDataContext.TaskFile
                    .Where(x =>
                        x.TaskId == task.TaskId)
                    .AsNoTracking()
                    .AsSingleQuery()
                    .ToListAsync(cancellationToken);
                task.TaskFiles = taskFiles;

                // get task reminders
                var taskReminders = await _ApprenticeProgressDataContext.TaskReminder
                    .Where(x =>
                        x.TaskId == task.TaskId)
                    .AsNoTracking()
                    .AsSingleQuery()
                    .ToListAsync(cancellationToken);
                task.TaskReminders = taskReminders;

                // get linked ksbs
                var taskLinkedKsbs = await _ApprenticeProgressDataContext.TaskKSBs
                    .Where(x =>
                        x.TaskId == task.TaskId)
                    .AsNoTracking()
                    .AsSingleQuery()
                    .ToListAsync(cancellationToken);
                task.TaskLinkedKsbs = taskLinkedKsbs;
            }

            var result = new GetTasksByApprenticeshipIdResult
            {
                Tasks = tasks
            };

            return result;
        }
    }
}