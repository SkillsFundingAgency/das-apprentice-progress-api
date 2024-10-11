using System.Diagnostics.CodeAnalysis;
using SFA.DAS.Http.Configuration;

namespace SFA.DAS.ApprenticeProgress.Functions.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class ApplicationSettings
    {
        public ApprenticeProgressApiOptions ApprenticeProgressApi { get; set; } = null!;
    }

    public class ApprenticeProgressApiOptions : IApimClientConfiguration
    {
        public const string ApprenticeProgressApi = "ApprenticeProgressApi";
        public string ApiBaseUrl { get; set; } = null!;
        public string SubscriptionKey { get; set; } = null!;
        public string ApiVersion { get; set; } = null!;
    }
}