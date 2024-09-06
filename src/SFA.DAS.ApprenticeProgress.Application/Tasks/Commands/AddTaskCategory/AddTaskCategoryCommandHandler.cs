using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ApprenticeProgress.Application.Queries;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Commands
{
    public class AddTaskCategoryCommandHandler : IRequestHandler<AddTaskCategoryCommand, Unit>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;
        
        public AddTaskCategoryCommandHandler
        (
            ApprenticeProgressDataContext ApprenticeProgressDataContext
        )
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }

        public Task<Unit> Handle(AddTaskCategoryCommand request, CancellationToken cancellationToken)
        {
            var task = new Domain.Entities.TaskCategory
            {
                TaskId = request.TaskId,
                CategoryId = request.CategoryId
            };

            _ApprenticeProgressDataContext.Add(task);
            _ApprenticeProgressDataContext.SaveChanges();

            return Task.FromResult(Unit.Value);
        }
    }
}