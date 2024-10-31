using System;
using System.Diagnostics.CodeAnalysis;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.FindAnApprenticeship.Api.AppStart
{
    public static class AddOpenTelemetryExtensions
    {
        /// <summary>
        /// Add the OpenTelemetry telemetry service to the application.
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <param name="appInsightsConnectionString">Azure app insights connection string.</param>
        public static void AddOpenTelemetryRegistration(this IServiceCollection services, string appInsightsConnectionString)
        {
            if (!string.IsNullOrEmpty(appInsightsConnectionString))
            {
                // This service will collect and send telemetry data to Azure Monitor.
                services.AddOpenTelemetry().UseAzureMonitor(options =>
                {
                    options.ConnectionString = appInsightsConnectionString;
                });
                // Configure metrics

            }

        }
    }
}
