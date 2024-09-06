using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.ApprenticeProgress.Domain.Configuration
{
    [ExcludeFromCodeCoverage]
    public class ApprenticeProgressConfiguration
    {
        public string SqlConnectionString { get ; set ; }
    }
}