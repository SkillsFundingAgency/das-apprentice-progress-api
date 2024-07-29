using System;
using MediatR;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetTaskCategoryQuery : IRequest<GetTaskCategoryResult>
    {
        public int TaskId { get; set; }
    }
}