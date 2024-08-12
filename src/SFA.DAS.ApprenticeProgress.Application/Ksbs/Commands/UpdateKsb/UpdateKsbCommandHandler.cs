using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Application.Commands
{
    public class UpdateKsbCommandHandler : IRequestHandler<UpdateKsbCommand, Unit>
    {
        private readonly ApprenticeProgressDataContext _ApprenticeProgressDataContext;

        public UpdateKsbCommandHandler
        (
            ApprenticeProgressDataContext ApprenticeProgressDataContext
        )
        {
            _ApprenticeProgressDataContext = ApprenticeProgressDataContext;
        }

        public async Task<Unit> Handle(UpdateKsbCommand request, CancellationToken cancellationToken)
        {
            var categories = await _ApprenticeProgressDataContext.ApprenticeshipCategory
                .AsNoTracking()
                .AsSingleQuery()
                .ToListAsync(cancellationToken);

            return Unit.Value;
        }
    }
}