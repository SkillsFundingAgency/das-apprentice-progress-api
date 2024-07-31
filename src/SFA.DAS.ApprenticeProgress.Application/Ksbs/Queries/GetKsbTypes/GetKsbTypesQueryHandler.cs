using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetKsbTypesQueryHandler : IRequestHandler<GetKsbTypesQuery, GetKsbTypesResult>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;

        public GetKsbTypesQueryHandler(ApprenticeProgressDataContext ApprenticeProgressDataContext)
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }

        public async Task<GetKsbTypesResult> Handle(GetKsbTypesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _ApprenticeProgressDataContext.ApprenticeshipCategory
               // .Where(x =>
              //      x.ApprenticeshipId == request.ApprenticeshipId)
                .AsNoTracking()
                .AsSingleQuery()
                .ToListAsync(cancellationToken);

            var result = new GetKsbTypesResult
            {
                TaskCategories = categories
            };

            return result;
        }
    }
}