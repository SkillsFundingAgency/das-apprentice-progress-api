using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

using SFA.DAS.ApprenticeProgress.Application.Queries;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Commands

{
    public class RemoveTaskByApprentishipIdAndTaskIdCommandHandler : IRequestHandler<RemoveTaskByApprentishipIdAndTaskIdCommand, Unit>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;
        
        public RemoveTaskByApprentishipIdAndTaskIdCommandHandler
        (
            ApprenticeProgressDataContext ApprenticeProgressDataContext
        )
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
            
        }

        public Task<Unit> Handle(RemoveTaskByApprentishipIdAndTaskIdCommand request, CancellationToken cancellationToken)
        {
            // task
            var task = _ApprenticeProgressDataContext.Task
                .Where(x => x.TaskId == request.TaskId && x.ApprenticeshipId == request.ApprenticeshipId)
                .SingleOrDefault();
            _ApprenticeProgressDataContext.Remove(task);

            // files
            var taskFiles = _ApprenticeProgressDataContext.TaskFile
                 .Where(x => x.TaskId == request.TaskId)
                 .ToListAsync();
            _ApprenticeProgressDataContext.RemoveRange(taskFiles);

            // linked ksbs
            var linkedKsbs = _ApprenticeProgressDataContext.TaskKSBs
                 .Where(x => x.TaskId == request.TaskId)
                 .ToListAsync();
            _ApprenticeProgressDataContext.RemoveRange(linkedKsbs);

            // reminders
            var reminders = _ApprenticeProgressDataContext.TaskReminder
                 .Where(x => x.TaskId == request.TaskId)
                 .ToListAsync();
            _ApprenticeProgressDataContext.RemoveRange(reminders);

            _ApprenticeProgressDataContext.SaveChanges();

            return Task.FromResult(Unit.Value);
        }
    }
}