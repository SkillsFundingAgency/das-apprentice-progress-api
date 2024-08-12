using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Commands
{
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

        public Task<Unit> Handle(AddTaskByApprenticeshipIdCommand request, CancellationToken cancellationToken)
        {
            var task = new Domain.Entities.Task
            {
                ApprenticeshipId = request.ApprenticeshipId,
                DueDate = request.DueDate,
                Title = request.Title,
                Note = request.Note,
                CompletionDateTime = request.CompletionDateTime,
                ApprenticeshipCategoryId = request.ApprenticeshipCategoryId,
                CreatedDateTime = DateTime.UtcNow,
                Status = (Domain.Entities.Task.TaskStatus)request.Status
            };

            _ApprenticeProgressDataContext.Add(task);
            _ApprenticeProgressDataContext.SaveChanges();

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

            var taskReminder = new Domain.Entities.TaskReminder
            {
                TaskId = (int)task.TaskId,
                ReminderUnit = (Domain.Entities.ReminderUnit)request.ReminderUnit,
                ReminderValue = request.ReminderValue,
                Status = (Domain.Entities.ReminderStatus?)(int)request.ReminderStatus
            };

            _ApprenticeProgressDataContext.Add(taskReminder);
            _ApprenticeProgressDataContext.SaveChanges();


            if (request.KsbsLinked != null)
            {
                foreach (var ksb in request.KsbsLinked)
                {
                    var ksbkey = new Guid(ksb);

                    var ksbprogressitems
                               = _ApprenticeProgressDataContext.KSBProgress
                                .Where(x => x.KSBId == ksbkey && x.ApprenticeshipId == request.ApprenticeshipId)
                                .FirstOrDefault();

                    if (ksbprogressitems != null)
                    {
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

            return Task.FromResult(Unit.Value);
        }
    }
}