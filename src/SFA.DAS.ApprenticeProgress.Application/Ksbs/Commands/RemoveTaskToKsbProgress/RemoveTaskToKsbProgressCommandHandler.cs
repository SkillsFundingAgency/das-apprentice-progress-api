using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Commands
{
    public class RemoveTaskToKsbProgressCommandHandler : IRequestHandler<RemoveTaskToKsbProgressCommand, Unit>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;

        public RemoveTaskToKsbProgressCommandHandler
        (
            ApprenticeProgressDataContext ApprenticeProgressDataContext
        )
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }

        public async Task<Unit> Handle(RemoveTaskToKsbProgressCommand request, CancellationToken cancellationToken)
        {
            var taskKsb = _ApprenticeProgressDataContext.TaskKSBs
           .Where(x => x.TaskId == request.TaskId && x.KSBProgressId == request.KsbProgressId)
           .SingleOrDefault();

            if (taskKsb != null)
                _ApprenticeProgressDataContext.Remove(taskKsb);

            return await Task.FromResult(Unit.Value);
        }
    }
}