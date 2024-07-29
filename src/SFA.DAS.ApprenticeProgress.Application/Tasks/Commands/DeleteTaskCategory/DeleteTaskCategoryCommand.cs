using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class DeleteTaskCategoryCommand : IRequest<Unit>
    {
        public int TaskId { get; set; }
    }
}