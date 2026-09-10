using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeProgress.Application.Models
{
    public class ContentfulNotification
    {
        public string Header { get; set; }
        public Document NotificationBody { get; set; }
    }
}
