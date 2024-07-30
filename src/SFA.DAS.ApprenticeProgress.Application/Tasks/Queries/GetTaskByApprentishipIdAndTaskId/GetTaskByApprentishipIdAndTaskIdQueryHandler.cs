using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetTaskByApprentishipIdAndTaskIdQueryHandler : IRequestHandler<GetTaskByApprentishipIdAndTaskIdQuery, GetTaskByApprentishipIdAndTaskIdResult>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;
        
        public GetTaskByApprentishipIdAndTaskIdQueryHandler(ApprenticeProgressDataContext ApprenticeProgressDataContext)
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
            
        }

        public async Task<GetTaskByApprentishipIdAndTaskIdResult> Handle(GetTaskByApprentishipIdAndTaskIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _ApprenticeProgressDataContext.Task
                .Where(x =>
                    x.ApprenticeshipId == request.ApprenticeshipId
                    &&
                    x.TaskId == request.TaskId)
                .AsNoTracking()
                .AsSingleQuery()
                .SingleAsync(cancellationToken);

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

            var result = new GetTaskByApprentishipIdAndTaskIdResult
            {
                Tasks = new List<Domain.Entities.Task>(){ task }
            };

            return result;
        }
    }
}
