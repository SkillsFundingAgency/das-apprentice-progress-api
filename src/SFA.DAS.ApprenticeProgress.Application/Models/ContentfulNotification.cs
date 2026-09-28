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
        public string Heading { get; set; }
        public string Description { get; set; }
        public string? Slug { get; set; }
    }
}
