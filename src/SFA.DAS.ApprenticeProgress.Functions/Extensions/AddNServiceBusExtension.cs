using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using SFA.DAS.ApprenticeProgress.Functions.Configuration;
using SFA.DAS.NServiceBus.Configuration;
using SFA.DAS.NServiceBus.Configuration.AzureServiceBus;
using SFA.DAS.NServiceBus.Configuration.NewtonsoftJsonSerializer;
using SFA.DAS.PushNotifications.Messages.Commands;

namespace SFA.DAS.ApprenticeProgress.Functions.Extensions;

[ExcludeFromCodeCoverage]
internal static class AddNServiceBusExtension
{
    public const string EndpointName = "SFA.DAS.LearnerNotifications.LearnerNotificationService";
    //public const string EndpointName = "SFA.DAS.PushNotifications";
    public static void AddNServiceBus(this IServiceCollection services, IConfiguration configuration)
    {
        //NServiceBusConfiguration nServiceBusConfiguration = new();
        //configuration.GetSection(nameof(NServiceBusConfiguration)).Bind(nServiceBusConfiguration);

        var connectionString = configuration["NServiceBusConnectionString"];
        var license = configuration["NServiceBusLicense"];

        var endpointConfiguration = new EndpointConfiguration(EndpointName)
                .UseErrorQueue($"{EndpointName}-errors")
                .UseInstallers()
                .UseMessageConventions()
                .UseNewtonsoftJsonSerializer();

        if (!string.IsNullOrEmpty(license))
        {
            endpointConfiguration.UseLicense(license);
        }

        endpointConfiguration.SendOnly();

        var startServiceBusEndpoint = false;

        if (configuration["EnvironmentName"] == "LOCAL")
        {
            var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            transport.UseWebSockets();
            transport.Routing().RouteToEndpoint(typeof(SendNotificationCommand), EndpointName);
            //transport.Routing().RouteToEndpoint(typeof(SendPushNotificationCommand), EndpointName);
            //var connectionString = nServiceBusConfiguration.NServiceBusConnectionString;            
            transport.ConnectionString(connectionString);
            startServiceBusEndpoint = true;
        }
        else
        {
            endpointConfiguration.UseAzureServiceBusTransport(connectionString, s => s.AddRouting());
            startServiceBusEndpoint = true;
        }

        if (startServiceBusEndpoint)
        {
            var test = connectionString;

            var endpointInstance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

            services
                .AddSingleton(p => endpointInstance)
                .AddSingleton<IMessageSession>(p => p.GetService<IEndpointInstance>());
        }
    }
}

[ExcludeFromCodeCoverage]
public static class RoutingSettingsExtensions
{
    public const string EndpointName = "SFA.DAS.LearnerNotifications.LearnerNotificationService";
    //public const string EndpointName = "SFA.DAS.PushNotifications";

    public static void AddRouting(this RoutingSettings routingSettings)
    {
        //routingSettings.RouteToEndpoint(typeof(SendPushNotificationCommand), EndpointName);
        routingSettings.RouteToEndpoint(typeof(SendNotificationCommand), EndpointName);
    }
}
