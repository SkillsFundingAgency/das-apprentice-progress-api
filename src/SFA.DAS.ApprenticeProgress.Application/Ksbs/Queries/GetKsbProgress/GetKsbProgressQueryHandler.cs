using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetKsbProgressQueryHandler : IRequestHandler<GetKsbProgressQuery, GetKsbProgressResult>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;
        
        public GetKsbProgressQueryHandler(ApprenticeProgressDataContext ApprenticeProgressDataContext)
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;

        }

        public async Task<GetKsbProgressResult> Handle(GetKsbProgressQuery request, CancellationToken cancellationToken)
        {
            var ksbProgress
               = await _ApprenticeProgressDataContext.KSBProgress
                .Where(x =>
                    x.ApprenticeshipId == request.ApprenticeshipId)
                .AsNoTracking()
                .AsSingleQuery()
                .ToListAsync(cancellationToken);

            var result = new GetKsbProgressResult
            {
                KsbProgress = ksbProgress
            };

            return result;
        }
    }
}
