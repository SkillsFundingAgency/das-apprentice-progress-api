using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeProgress.Application.Queries;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Commands

{
    public class UpdateTaskStatusCommandHandler : IRequestHandler<UpdateTaskStatusCommand, Unit>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;
        
        public UpdateTaskStatusCommandHandler
        (
            ApprenticeProgressDataContext ApprenticeProgressDataContext
        )
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }

        public async Task<Unit> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var task = await _ApprenticeProgressDataContext.Task
                .Where(x => x.TaskId == request.TaskId)
                .SingleOrDefaultAsync(cancellationToken);

            if (task != null)
            {
                task.Status = (Domain.Entities.Task.TaskStatus)request.Status;
                _ApprenticeProgressDataContext.SaveChanges();
            }

            return await Task.FromResult(Unit.Value);
        }
    }
}