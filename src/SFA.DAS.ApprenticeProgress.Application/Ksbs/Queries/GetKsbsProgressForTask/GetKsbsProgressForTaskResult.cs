using System.Collections.Generic;
using SFA.DAS.ApprenticeProgress.Application.Models;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetKsbsProgressForTaskResult : QueryResult<GetKsbsProgressForTaskResult>
    {
        public List<Domain.Entities.KSBProgress> KsbProgress { get; set; }
    }
}
