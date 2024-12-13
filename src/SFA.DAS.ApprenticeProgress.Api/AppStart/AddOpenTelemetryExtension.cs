﻿using Azure.Monitor.OpenTelemetry.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace SFA.DAS.FindAnApprenticeship.Api.AppStart
{
    public static class AddOpenTelemetryExtensions
    {
        public static void AddOpenTelemetryRegistration(this IServiceCollection services, string APPLICATIONINSIGHTS_CONNECTION_STRING)
        {
           services.AddOpenTelemetry().UseAzureMonitor(options =>
           {
             options.ConnectionString = APPLICATIONINSIGHTS_CONNECTION_STRING;
           });
        }
    }
}
