using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.ApprenticeProgress.Functions.Infrastructure;

[ExcludeFromCodeCoverage]
public static class ConfigurationExtensions
{
    public static bool IsAcceptanceTest(this IConfiguration config)
    {
        return config["EnvironmentName"].Equals("ACCEPTANCE_TESTS", StringComparison.CurrentCultureIgnoreCase);
    }

    public static bool IsLocalAcceptanceOrDev(this IConfiguration config)
    {
        return config["EnvironmentName"].Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase) ||
               config["EnvironmentName"].Equals("ACCEPTANCE_TESTS", StringComparison.CurrentCultureIgnoreCase) ||
               config["EnvironmentName"].Equals("DEV", StringComparison.CurrentCultureIgnoreCase);
    }

    public static bool IsAcceptanceOrDev(this IConfiguration config)
    {
        return config["EnvironmentName"].Equals("ACCEPTANCE_TESTS", StringComparison.CurrentCultureIgnoreCase) ||
               config["EnvironmentName"].Equals("DEV", StringComparison.CurrentCultureIgnoreCase);
    }
}