using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeProgress.Functions.Api.Response
{
    public class GetProgressNotificationsWrapper
    {
        public List<Domain.Entities.ApprenticeshipProgressNotification> Notifications { get; set; }
    }
}
