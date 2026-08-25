using System;
using System.Collections.Generic;

namespace SFA.DAS.ApprenticeProgress.Application.Notifications.Queries.GetProgressNotificationsToCheck
{
    public class GetProgressNotificationsToCheckResult
    {
        public List<Domain.Entities.ApprenticeshipProgressNotification> Notifications { get; set; }
    }
}
