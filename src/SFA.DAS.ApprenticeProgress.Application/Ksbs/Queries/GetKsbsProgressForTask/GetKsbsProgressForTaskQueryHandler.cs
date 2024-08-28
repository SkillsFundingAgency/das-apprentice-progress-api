using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetKsbsProgressForTaskQueryHandler : IRequestHandler<GetKsbsProgressForTaskQuery, GetKsbsProgressForTaskResult>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;
        
        public GetKsbsProgressForTaskQueryHandler(ApprenticeProgressDataContext ApprenticeProgressDataContext)
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }

        public async Task<GetKsbsProgressForTaskResult> Handle(GetKsbsProgressForTaskQuery request, CancellationToken cancellationToken)
        {

            var ksbJoins
               = await _ApprenticeProgressDataContext.TaskKSBs
                .Where(x =>
                    x.TaskId == request.TaskId)
                .ToListAsync(cancellationToken);

            List<Domain.Entities.KSBProgress> list = new List<Domain.Entities.KSBProgress>();

            foreach (var join in ksbJoins)
            {
                var ksbprogress
                   = await _ApprenticeProgressDataContext.KSBProgress
                    .Where(x =>
                        x.KSBProgressId == join.KSBProgressId)
                    .FirstOrDefaultAsync();
                list.Add(ksbprogress);
            }

            var ksbProgress
               = await _ApprenticeProgressDataContext.KSBProgress
                .Where(x =>
                    x.ApprenticeshipId == request.ApprenticeshipId)
                .AsNoTracking()
                .AsSingleQuery()
                .ToListAsync(cancellationToken);

            var result = new GetKsbsProgressForTaskResult
            {
                KsbProgress = list
            };

            return result;
        }
    }
}