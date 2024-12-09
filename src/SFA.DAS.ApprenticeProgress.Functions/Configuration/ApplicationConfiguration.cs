using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.ApprenticeProgress.Functions.Configuration;

[ExcludeFromCodeCoverage]
public class ApplicationConfiguration
{
    public ApprenticeProgressApiConfiguration ApprenticeProgressApiConfiguration { get; set; }
}