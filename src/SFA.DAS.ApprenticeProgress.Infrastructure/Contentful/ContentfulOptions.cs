using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.ApprenticeProgress.Infrastructure.Contentful
{
    public class ContentfulOptions
    {
        public string SpaceId { get; set; } = string.Empty;
        public string Environment { get; set; } = "master";
        public string DeliveryApiKey { get; set; } = string.Empty;
    }
}
