using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ApprenticeProgress.Application.Queries;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Commands
{
    public class RemoveTaskByApprenticeshipIdAndTaskIdCommandHandler : IRequestHandler<RemoveTaskByApprenticeshipIdAndTaskIdCommand, Unit>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;
        
        public RemoveTaskByApprenticeshipIdAndTaskIdCommandHandler
        (
            ApprenticeProgressDataContext ApprenticeProgressDataContext
        )
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }

        public Task<Unit> Handle(RemoveTaskByApprenticeshipIdAndTaskIdCommand request, CancellationToken cancellationToken)
        {
            var task = _ApprenticeProgressDataContext.Task
                .Where(x => x.TaskId == request.TaskId && x.ApprenticeshipId == request.ApprenticeshipId)
                .SingleOrDefault();

            if (task != null)
                _ApprenticeProgressDataContext.Remove(task);

            var taskFiles = _ApprenticeProgressDataContext.TaskFile
                 .Where(x => x.TaskId == request.TaskId)
                 .ToList();

            if (taskFiles != null)
                _ApprenticeProgressDataContext.RemoveRange(taskFiles);

            var linkedKsbs = _ApprenticeProgressDataContext.TaskKSBs
                 .Where(x => x.TaskId == request.TaskId)
                 .ToList();
            if (linkedKsbs != null)
                _ApprenticeProgressDataContext.RemoveRange(linkedKsbs);

            var reminders = _ApprenticeProgressDataContext.TaskReminder
                 .Where(x => x.TaskId == request.TaskId)
                 .ToList();
            if (reminders != null)
                _ApprenticeProgressDataContext.RemoveRange(reminders);

            _ApprenticeProgressDataContext.SaveChanges();

            return Task.FromResult(Unit.Value);
        }
    }
}