using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetTaskByApprenticeshipIdAndTaskIdQueryHandler : IRequestHandler<GetTaskByApprenticeshipIdAndTaskIdQuery, GetTaskByApprenticeshipIdAndTaskIdResult>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;
        
        public GetTaskByApprenticeshipIdAndTaskIdQueryHandler(ApprenticeProgressDataContext ApprenticeProgressDataContext)
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;   
        }

        public async Task<GetTaskByApprenticeshipIdAndTaskIdResult> Handle(GetTaskByApprenticeshipIdAndTaskIdQuery request, CancellationToken cancellationToken)
        {
           var task = await _ApprenticeProgressDataContext.Task
               .Include(t => t.TaskFiles)
               .Include(t => t.TaskReminders)
               .Include(t => t.TaskLinkedKsbs)
               .Where(x =>
                   x.ApprenticeshipId == request.ApprenticeshipId &&
                   x.TaskId == request.TaskId)
               .AsNoTracking()
               .FirstOrDefaultAsync(cancellationToken);
           
           task.ApprenticeshipCategory = await _ApprenticeProgressDataContext.ApprenticeshipCategory
               .Where(x => x.CategoryId == task.ApprenticeshipCategoryId)
               .ToListAsync(cancellationToken);
           
           var result = new GetTaskByApprenticeshipIdAndTaskIdResult
           {
               Tasks = new List<Domain.Entities.Task>(){ task }
           };

            return result;
        } 
    }
}
