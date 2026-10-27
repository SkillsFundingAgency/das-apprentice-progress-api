using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFA.DAS.ApprenticeProgress.Application.Models;

namespace SFA.DAS.ApprenticeProgress.Application.Interfaces
{
    public interface IContentfulService
    {
        Task<ContentfulNotification> GetContentAsync(string notificationId);
    }
}
