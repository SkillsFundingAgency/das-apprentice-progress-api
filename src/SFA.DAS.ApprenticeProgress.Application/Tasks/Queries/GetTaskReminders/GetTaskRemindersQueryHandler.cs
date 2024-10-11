using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Polly;
using SFA.DAS.ApprenticeProgress.Application.Models;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetTaskRemindersQueryHandler : IRequestHandler<GetTaskRemindersQuery, GetTaskRemindersResult>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;
        
        public GetTaskRemindersQueryHandler(ApprenticeProgressDataContext ApprenticeProgressDataContext)
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;   
        }

        public async Task<GetTaskRemindersResult> Handle(GetTaskRemindersQuery request, CancellationToken cancellationToken)
        {

            List<TaskReminderModel> reminders = new List<TaskReminderModel>();

            var taskReminders = await _ApprenticeProgressDataContext.TaskReminder
                .Where(x =>
                     x.Status == Domain.Entities.ReminderStatus.NotSent
                     // TODO AND IN BETWEEN UPCOMING MINUTE OF THE TASK DUE TIME
                     )
                .AsNoTracking()
                .AsSingleQuery()
                .ToListAsync(cancellationToken);

            foreach(var reminder in taskReminders)
            {
                var task = await _ApprenticeProgressDataContext.Task.Where(x => x.TaskId == reminder.TaskId).SingleOrDefaultAsync(cancellationToken: cancellationToken);

                reminders.Add(new TaskReminderModel()
                {
                     TaskId = task.TaskId,
                     ApprenticeshipId = task.ApprenticeshipId,
                     DueDate = task.DueDate,
                     Title = task.Title,
                     ApprenticeshipCategoryId = task.ApprenticeshipCategoryId,
                     Note = task.Note,
                     CompletionDateTime = task.CompletionDateTime,
                     CreatedDateTime = task.CreatedDateTime,
                     ReminderValue = reminder.ReminderValue,
                     ReminderUnit = reminder.ReminderUnit,
                     ReminderStatus = reminder.Status
                }
                );
            }

            var result = new GetTaskRemindersResult
            {
                TaskReminders = reminders
            };

            return result;
        }
    }
}