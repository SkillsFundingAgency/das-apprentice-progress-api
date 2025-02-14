using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetKsbStatusesQueryHandler : IRequestHandler<GetKsbStatusesQuery, GetKsbStatusesResult>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;
        
        public GetKsbStatusesQueryHandler(ApprenticeProgressDataContext ApprenticeProgressDataContext)
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }

        public async Task<GetKsbStatusesResult> Handle(GetKsbStatusesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _ApprenticeProgressDataContext.ApprenticeshipCategory
                .AsSingleQuery()
                .ToListAsync(cancellationToken);

            var result = new GetKsbStatusesResult
            {
                TaskCategories = categories
            };

            return result;
        }
    }
}
