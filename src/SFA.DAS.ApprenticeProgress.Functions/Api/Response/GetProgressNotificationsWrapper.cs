using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.ApprenticeProgress.Functions.Api.Response
{
    [ExcludeFromCodeCoverage]
    public class GetProgressNotificationsWrapper
    {
        public List<Domain.Entities.ApprenticeshipProgressNotification> Notifications { get; set; }
    }
}
