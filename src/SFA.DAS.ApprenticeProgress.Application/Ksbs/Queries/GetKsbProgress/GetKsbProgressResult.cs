using System.Collections.Generic;
using SFA.DAS.ApprenticeProgress.Application.Models;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetKsbProgressResult : QueryResult<GetKsbProgressResult>
    {
        public List<Domain.Entities.KSBProgress> KsbProgress { get; set; }
    }
}
