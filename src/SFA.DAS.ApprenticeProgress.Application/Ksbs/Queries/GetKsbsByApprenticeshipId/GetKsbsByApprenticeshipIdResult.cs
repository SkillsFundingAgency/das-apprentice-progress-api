using System.Collections.Generic;
using SFA.DAS.ApprenticeProgress.Application.Models;

namespace SFA.DAS.ApprenticeProgress.Application.Queries
{
    public class GetKsbsByApprenticeshipIdResult : QueryResult<GetKsbsByApprenticeshipIdResult>
    {
        public List<Domain.Entities.KSBProgress> KSBProgresses { get; set; }
    }
}
