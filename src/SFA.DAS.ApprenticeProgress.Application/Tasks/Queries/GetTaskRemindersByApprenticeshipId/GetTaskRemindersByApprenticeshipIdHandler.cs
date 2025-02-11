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
            List<TaskReminderModel> reminders = new List<TaskReminderModel>();

            var tasks = await _ApprenticeProgressDataContext.Task
                .Include(t => t.TaskReminders)
                .Where(task => task.ApprenticeshipId == request.ApprenticeshipId && task.Status == Domain.Entities.Task.TaskStatus.Todo)
                .Join(_ApprenticeProgressDataContext.TaskReminder, task => task.TaskId, reminder => reminder.TaskId, (task, reminder) => new { task, reminder })
                .Where(r => r.reminder.Status == Domain.Entities.ReminderStatus.Sent)
                .AsNoTracking()
                .AsSingleQuery()
                .ToListAsync(cancellationToken);
            
         
            

            if (tasks.Count > 0)
            {
                foreach (var task in tasks)
                {
                    
                    TaskReminderModel taskReminderModel = new TaskReminderModel();
                    taskReminderModel.TaskId = task.task.TaskId;
                    taskReminderModel.ApprenticeshipId = request.ApprenticeshipId;
                    taskReminderModel.ApprenticeAccountId = task.task.ApprenticeAccountId;
                    taskReminderModel.DueDate = task.task.DueDate;
                    taskReminderModel.ApprenticeshipCategoryId = 1;
                    taskReminderModel.Title = task.task.Title;
                    taskReminderModel.ReminderValue = task.reminder.ReminderValue;
                    taskReminderModel.ReminderStatus = task.reminder.Status;
                    reminders.Add(taskReminderModel);
                }
            }
            
            var result = new GetTaskRemindersByApprenticeshipIdResult
            {
                TaskReminders = reminders
            };

            return result;
        }
    }
}
