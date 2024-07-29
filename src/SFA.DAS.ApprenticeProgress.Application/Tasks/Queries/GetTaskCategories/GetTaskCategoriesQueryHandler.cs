using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetTaskCategoriesQueryHandler : IRequestHandler<GetTaskCategoriesQuery, GetTaskCategoriesResult>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;
        
        public GetTaskCategoriesQueryHandler(ApprenticeProgressDataContext ApprenticeProgressDataContext)
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }

        public async Task<GetTaskCategoriesResult> Handle(GetTaskCategoriesQuery request, CancellationToken cancellationToken)
        {
            var taskCategories = await _ApprenticeProgressDataContext.ApprenticeshipCategory
                .AsNoTracking()
                .AsSingleQuery()
                .ToListAsync(cancellationToken);

            var result = new GetTaskCategoriesResult
            {
                TaskCategories = taskCategories
            };

            return result;
        }
    }
}
