using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeProgress.Application.Notifications.Queries.GetApprenticeshipProgressNotifications
{
    public class GetApprenticeshipProgressNotificationsByIdResult
    {
        public Domain.Entities.ApprenticeshipProgress ApprenticeshipProgress { get; set; }
        public List<Domain.Entities.ApprenticeshipProgressNotification> ApprenticeshipProgressNotification { get; set; }
    }
}
