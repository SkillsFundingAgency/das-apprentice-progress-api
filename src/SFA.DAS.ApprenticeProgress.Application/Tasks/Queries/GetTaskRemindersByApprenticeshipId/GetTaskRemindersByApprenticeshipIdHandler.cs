using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeProgress.Application.Models;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Tasks.Queries
{
    public class GetTaskRemindersByApprenticeshipIdHandler : IRequestHandler<GetTaskRemindersByApprenticeshipIdQuery, GetTaskRemindersByApprenticeshipIdResult>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;

        public GetTaskRemindersByApprenticeshipIdHandler(ApprenticeProgressDataContext ApprenticeProgressDataContext)
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }
        
        public async Task<GetTaskRemindersByApprenticeshipIdResult> Handle(GetTaskRemindersByApprenticeshipIdQuery request, CancellationToken cancellationToken)
        {
            var query = _ApprenticeProgressDataContext.Task
            .Include(t => t.TaskReminders)
            .Where(t => t.ApprenticeshipId == request.ApprenticeshipId && t.Status == Domain.Entities.Task.TaskStatus.Todo)
            .SelectMany(t => t.TaskReminders
            .Where(r => r.Status == Domain.Entities.ReminderStatus.Sent)
            .Select(r => new TaskReminderModel
            {
                TaskId = t.TaskId,
                ApprenticeshipId = t.ApprenticeshipId,
                ApprenticeAccountId = t.ApprenticeAccountId,
                DueDate = t.DueDate,
                Title = t.Title,
                ReminderValue = r.ReminderValue,
                ReminderStatus = r.Status,
                ApprenticeshipCategoryId = 1
            }))
            .AsNoTracking();

            var reminders = await query.ToListAsync(cancellationToken);

            return new GetTaskRemindersByApprenticeshipIdResult
            {
                TaskReminders = reminders
            };
        } 
    }
}
