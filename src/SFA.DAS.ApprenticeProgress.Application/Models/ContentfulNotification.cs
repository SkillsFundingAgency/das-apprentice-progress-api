using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.ApprenticeProgress.Application.Models
{
    [ExcludeFromCodeCoverage]
    public class ContentfulNotification
    {
        public string Heading { get; set; }
        public string Description { get; set; }
        public string? Slug { get; set; }
    }
}
