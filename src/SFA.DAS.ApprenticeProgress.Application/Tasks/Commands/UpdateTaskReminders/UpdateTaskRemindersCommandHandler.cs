using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeProgress.Application.Queries;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Commands
{
    public class UpdateTaskRemindersCommandHandler : IRequestHandler<UpdateTaskRemindersCommand, Unit>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;
        
        public UpdateTaskRemindersCommandHandler
        (
            ApprenticeProgressDataContext ApprenticeProgressDataContext
        )
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }

        public async Task<Unit> Handle(UpdateTaskRemindersCommand request, CancellationToken cancellationToken)
        {
            var taskReminder = await _ApprenticeProgressDataContext.TaskReminder
                .Where(x => x.TaskId == request.TaskId)
                .SingleOrDefaultAsync(cancellationToken);

            if (taskReminder != null)
            {
                taskReminder.Status = (Domain.Entities.ReminderStatus?)request.StatusId;
                _ApprenticeProgressDataContext.SaveChanges();
            }

            return await Task.FromResult(Unit.Value);
        }
    }
}
