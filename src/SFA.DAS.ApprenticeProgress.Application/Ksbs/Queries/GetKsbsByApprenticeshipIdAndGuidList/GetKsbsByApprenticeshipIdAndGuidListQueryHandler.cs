using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetKsbsByApprenticeshipIdAndGuidListQueryHandler : IRequestHandler<GetKsbsByApprenticeshipIdAndGuidListQuery, GetKsbsByApprenticeshipIdAndGuidListResult>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;

        public GetKsbsByApprenticeshipIdAndGuidListQueryHandler(ApprenticeProgressDataContext ApprenticeProgressDataContext)
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }

        public async Task<GetKsbsByApprenticeshipIdAndGuidListResult> Handle(GetKsbsByApprenticeshipIdAndGuidListQuery request, CancellationToken cancellationToken)
        {
            var ksbs = await _ApprenticeProgressDataContext.KSBProgress
                .Where(x =>
                    x.ApprenticeshipId == request.ApprenticeshipId
                    &&
                    request.KsbIds.Any(y => x.KSBId == y)
                    )
                .ToListAsync(cancellationToken);

            var result = new GetKsbsByApprenticeshipIdAndGuidListResult
            {
                KSBProgresses = ksbs
            };

            return result;
        }
    }
}