﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeProgress.Application.Queries;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Commands
{
    public class UpdateTaskByApprenticeshipIdAndTaskIdCommandHandler : IRequestHandler<UpdateTaskByApprenticeshipIdAndTaskIdCommand, Unit>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;
        
        public UpdateTaskByApprenticeshipIdAndTaskIdCommandHandler
        (
            ApprenticeProgressDataContext ApprenticeProgressDataContext
        )
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }

        public async Task<Unit> Handle(UpdateTaskByApprenticeshipIdAndTaskIdCommand request, CancellationToken cancellationToken)
        {
            var task = await _ApprenticeProgressDataContext.Task
                .Where(x =>
                x.ApprenticeshipId == request.ApprenticeshipId
                &&
                x.TaskId == request.TaskId)
                .SingleOrDefaultAsync(cancellationToken);

            if (task != null)
            {
                task.DueDate = request.DueDate;
                task.Title = request.Title;
                task.Note = request.Note;
                task.CompletionDateTime = request.CompletionDateTime;
                task.Status = (Domain.Entities.Task.TaskStatus)request.Status;
                task.ApprenticeshipCategoryId = request.ApprenticeshipCategoryId;

                _ApprenticeProgressDataContext.SaveChanges();
            }

            // first remove all asssets then re-save

            // files
            var taskFiles = _ApprenticeProgressDataContext.TaskFile
                 .Where(x => x.TaskId == request.TaskId)
                 .ToList();

            if (taskFiles != null)
                _ApprenticeProgressDataContext.RemoveRange(taskFiles);

            // linked ksbs
            var linkedKsbs = _ApprenticeProgressDataContext.TaskKSBs
                 .Where(x => x.TaskId == request.TaskId)
                 .ToList();

            if (linkedKsbs != null)
                _ApprenticeProgressDataContext.RemoveRange(linkedKsbs);

            // reminders
            var reminders = _ApprenticeProgressDataContext.TaskReminder
                 .Where(x => x.TaskId == request.TaskId)
                 .ToList();

            if (reminders != null)
                _ApprenticeProgressDataContext.RemoveRange(reminders);

            _ApprenticeProgressDataContext.SaveChanges();

            // add files
            if (task != null)
            {
                if (request.Files != null)
                {
                    foreach (var file in request.Files)
                    {
                        // add validation
                        var taskFile = new Domain.Entities.TaskFile
                        {
                            TaskId = (int)task.TaskId,
                            FileType = file.FileType,
                            FileName = file.FileName,
                            FileContents = Encoding.ASCII.GetBytes(file.FileContents)
                        };

                        _ApprenticeProgressDataContext.Add(taskFile);
                        _ApprenticeProgressDataContext.SaveChanges();
                    }
                }
            }

            // get the ksb first
            if (task != null && request.KsbsLinked != null && request.KsbsLinked[0] != null)
            {
                foreach (var ksb in request.KsbsLinked)
                    {
                        var ksbkey = new Guid(ksb);

                        // get the ksb progress item
                        var ksbprogressitems
                                   = _ApprenticeProgressDataContext.KSBProgress
                                    .Where(x => x.KSBId == ksbkey && x.ApprenticeshipId == request.ApprenticeshipId)
                                    .FirstOrDefault();

                        if (ksbprogressitems != null)
                        {
                            // add join
                            var taskKsbJoin = new Domain.Entities.TaskKSBs
                            {
                                TaskId = (int)task.TaskId,
                                KSBProgressId = ksbprogressitems.KSBProgressId
                            };
                            _ApprenticeProgressDataContext.Add(taskKsbJoin);
                            _ApprenticeProgressDataContext.SaveChanges();
                        }
                    }
            }

            // add reminder
            if (task != null)
            {
                if (request.ReminderValue != null)
                {
                    var taskReminder = new Domain.Entities.TaskReminder
                    {
                        TaskId = (int)task.TaskId,
                        ReminderUnit = Domain.Entities.ReminderUnit.Minutes,
                        ReminderValue = request.ReminderValue,
                        Status = Domain.Entities.ReminderStatus.NotSent
                    };

                    _ApprenticeProgressDataContext.Add(taskReminder);
                    _ApprenticeProgressDataContext.SaveChanges();
                }
            }

            return Unit.Value;
        }
    }
}
