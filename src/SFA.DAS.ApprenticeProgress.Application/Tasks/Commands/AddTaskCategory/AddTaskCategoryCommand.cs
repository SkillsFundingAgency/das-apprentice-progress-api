using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class AddTaskCategoryCommand : IRequest<Unit>
    {
        public int TaskId { get; set; }
        public int CategoryId { get; set; }
    }
}