using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.ApprenticeProgress.Infrastructure.Contentful
{
    [ExcludeFromCodeCoverage]
    public class ContentfulOptions
    {
        public string SpaceId { get; set; } = string.Empty;
        public string Environment { get; set; } = "master";
        public string DeliveryApiKey { get; set; } = string.Empty;
    }
}
