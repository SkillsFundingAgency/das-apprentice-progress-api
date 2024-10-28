using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.ApprenticeProgress.Functions.Configuration
{
    [ExcludeFromCodeCoverage]
    public class ApprenticeProgressApiConfiguration
    {
        public string Url { get; set; } = null!;
        public string Identifier { get; set; } = null!;
    }
}