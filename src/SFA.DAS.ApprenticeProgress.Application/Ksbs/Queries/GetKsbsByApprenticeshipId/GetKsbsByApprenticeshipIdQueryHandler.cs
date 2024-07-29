using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetKsbsByApprenticeshipIdQueryHandler : IRequestHandler<GetKsbsByApprenticeshipIdQuery, GetKsbsByApprenticeshipIdResult>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;


        public GetKsbsByApprenticeshipIdQueryHandler(ApprenticeProgressDataContext ApprenticeProgressDataContext)
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }

        public async Task<GetKsbsByApprenticeshipIdResult> Handle(GetKsbsByApprenticeshipIdQuery request, CancellationToken cancellationToken)
        {
            var ksbs = await _ApprenticeProgressDataContext.KSBProgress
                .Where(x =>
                    x.ApprenticeshipId == request.ApprenticeshipId)
                .AsNoTracking()
                .AsSingleQuery()
                .ToListAsync(cancellationToken);

            var result = new GetKsbsByApprenticeshipIdResult
            {
                KSBProgresses = ksbs
            };

            return result;
        }
    }
}
