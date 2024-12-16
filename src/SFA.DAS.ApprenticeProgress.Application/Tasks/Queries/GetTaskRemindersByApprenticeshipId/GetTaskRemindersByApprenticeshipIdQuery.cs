using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Tasks.Queries
{
    public class GetTaskRemindersByApprenticeshipIdQuery : IRequest<GetTaskRemindersByApprenticeshipIdResult>
    {
        public long ApprenticeshipId { get; set; }
    }
}
