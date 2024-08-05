using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetTaskCategoryQueryHandler : IRequestHandler<GetTaskCategoryQuery, GetTaskCategoryResult>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;
        
        public GetTaskCategoryQueryHandler(ApprenticeProgressDataContext ApprenticeProgressDataContext)
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }

        public async Task<GetTaskCategoryResult> Handle(GetTaskCategoryQuery request, CancellationToken cancellationToken)
        {
            var taskCategories = await _ApprenticeProgressDataContext.TaskCategory
                .Where(x =>
                    x.TaskId == request.TaskId)
                .AsNoTracking()
                .AsSingleQuery()
                .ToListAsync(cancellationToken);

            var result = new GetTaskCategoryResult
            {
                TaskCategories = taskCategories
            };

            return result;
        }
    }
}
