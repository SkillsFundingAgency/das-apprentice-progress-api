using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

using SFA.DAS.ApprenticeProgress.Application.Queries;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Commands

{
    public class DeleteTaskCategoryCommandHandler : IRequestHandler<DeleteTaskCategoryCommand, Unit>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;
        
        public DeleteTaskCategoryCommandHandler
        (
            ApprenticeProgressDataContext ApprenticeProgressDataContext
        )
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
            
        }

        public Task<Unit> Handle(DeleteTaskCategoryCommand request, CancellationToken cancellationToken)
        {
            var task = _ApprenticeProgressDataContext.Task
                .Where(x =>
                    x.TaskId == request.TaskId)
                .AsNoTracking()
                .AsSingleQuery()
                .SingleOrDefault();
             _ApprenticeProgressDataContext.Remove(task);
             _ApprenticeProgressDataContext.SaveChanges();

            return Task.FromResult(Unit.Value);
        }
    }
}