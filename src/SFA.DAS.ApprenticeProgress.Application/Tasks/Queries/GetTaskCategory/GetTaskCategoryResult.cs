using System.Collections.Generic;
using SFA.DAS.ApprenticeProgress.Application.Models;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetTaskCategoryResult : QueryResult<GetTaskCategoryResult>
    {
        public List<Domain.Entities.TaskCategory> TaskCategories { get; set; }
    }
}