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

        /*    foreach (var tc in taskCategories)
            {
                var apprenticeshipCategories = await _ApprenticeProgressDataContext.ApprenticeshipCategory
                    .Where(x =>
                        x.CategoryId == tc.CategoryId)
                    .AsNoTracking()
                    .AsSingleQuery()
                    .ToListAsync(cancellationToken);

                tc.ApprenticeshipCategories = apprenticeshipCategories;
            }*/

            var result = new GetTaskCategoryResult
            {
                TaskCategories = taskCategories
            };

            return result;
        }
    }
}
