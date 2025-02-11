using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Commands
{
    [ExcludeFromCodeCoverage]
    public class AddTaskByApprenticeshipIdCommandHandler : IRequestHandler<AddTaskByApprenticeshipIdCommand, Unit>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;

        public AddTaskByApprenticeshipIdCommandHandler
        (
            ApprenticeProgressDataContext ApprenticeProgressDataContext
        )
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }

        public async Task<Unit> Handle(AddTaskByApprenticeshipIdCommand request, CancellationToken cancellationToken)
        {
            // Create and save the main task first
            var task = new Domain.Entities.Task
            {
                ApprenticeshipId = request.ApprenticeshipId,
                ApprenticeAccountId = request.ApprenticeAccountId,
                DueDate = request.DueDate,
                Title = request.Title,
                Note = request.Note,
                CompletionDateTime = request.CompletionDateTime,
                ApprenticeshipCategoryId = request.ApprenticeshipCategoryId,
                CreatedDateTime = DateTime.UtcNow,
                Status = (Domain.Entities.Task.TaskStatus)request.Status
            };

            _ApprenticeProgressDataContext.Add(task);
            await _ApprenticeProgressDataContext.SaveChangesAsync(cancellationToken);
            
            var taskId = (int)task.TaskId;

            var bulkOperations = new List<Task>();

            // Process files in bulk
            if (request.Files?.Count > 0)
            {
                var taskFiles = request.Files.Select(file => new Domain.Entities.TaskFile
                {
                    TaskId = taskId,
                    FileType = file.FileType,
                    FileName = file.FileName,
                    FileContents = Encoding.ASCII.GetBytes(file.FileContents)
                }).ToList();

                _ApprenticeProgressDataContext.AddRange(taskFiles);
            }

            // Process KSBs with a single query
            if (request.KsbsLinked != null && request.KsbsLinked[0] != null)
            {
                var ksbIds = request.KsbsLinked.Select(k => new Guid(k)).ToList();
                var ksbProgressItems = await _ApprenticeProgressDataContext.KSBProgress
                    .Where(x => ksbIds.Contains(x.KSBId) && x.ApprenticeshipId == request.ApprenticeshipId)
                    .ToListAsync(cancellationToken);

                var validKsbs = ksbProgressItems
                    .Where(k => ksbIds.Contains(k.KSBId))
                    .Select(k => new Domain.Entities.TaskKSBs
                    {
                        TaskId = taskId,
                        KSBProgressId = k.KSBProgressId
                    });

                _ApprenticeProgressDataContext.AddRange(validKsbs);
            }

            // Add reminder if needed
            if (request.ReminderValue != null)
            {
                _ApprenticeProgressDataContext.Add(new Domain.Entities.TaskReminder
                {
                    TaskId = taskId,
                    ReminderUnit = Domain.Entities.ReminderUnit.Minutes,
                    ReminderValue = request.ReminderValue,
                    Status = Domain.Entities.ReminderStatus.NotSent
                });
            }

            // Execute all remaining operations in a single transaction
            await _ApprenticeProgressDataContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
