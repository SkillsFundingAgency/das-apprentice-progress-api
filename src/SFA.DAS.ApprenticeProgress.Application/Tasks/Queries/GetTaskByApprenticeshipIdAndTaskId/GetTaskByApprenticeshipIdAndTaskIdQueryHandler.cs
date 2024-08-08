using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetTaskByApprenticeshipIdAndTaskIdQueryHandler : IRequestHandler<GetTaskByApprenticeshipIdAndTaskIdQuery, GetTaskByApprenticeshipIdAndTaskIdResult>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;
        
        public GetTaskByApprenticeshipIdAndTaskIdQueryHandler(ApprenticeProgressDataContext ApprenticeProgressDataContext)
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;   
        }

        public async Task<GetTaskByApprenticeshipIdAndTaskIdResult> Handle(GetTaskByApprenticeshipIdAndTaskIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _ApprenticeProgressDataContext.Task
                .Where(x =>
                    x.ApprenticeshipId == request.ApprenticeshipId
                    &&
                    x.TaskId == request.TaskId)
                .SingleOrDefaultAsync(cancellationToken);

            if (task != null)
            {
                var taskCategories = await _ApprenticeProgressDataContext.ApprenticeshipCategory
                .Where(x =>
                    x.CategoryId == task.ApprenticeshipCategoryId)
                .ToListAsync(cancellationToken);
                task.ApprenticeshipCategory = taskCategories;

                var taskFiles = await _ApprenticeProgressDataContext.TaskFile
                    .Where(x =>
                        x.TaskId == task.TaskId)
                    .ToListAsync(cancellationToken);
                task.TaskFiles = taskFiles;

                var taskReminders = await _ApprenticeProgressDataContext.TaskReminder
                    .Where(x =>
                        x.TaskId == task.TaskId)
                    .ToListAsync(cancellationToken);
                task.TaskReminders = taskReminders;

                var taskLinkedKsbs = await _ApprenticeProgressDataContext.TaskKSBs
                    .Where(x =>
                        x.TaskId == task.TaskId)
                    .ToListAsync(cancellationToken);
                task.TaskLinkedKsbs = taskLinkedKsbs;
            }

            var result = new GetTaskByApprenticeshipIdAndTaskIdResult
            {
                Tasks = new List<Domain.Entities.Task>(){ task }
            };

            return result;
        }
    }
}