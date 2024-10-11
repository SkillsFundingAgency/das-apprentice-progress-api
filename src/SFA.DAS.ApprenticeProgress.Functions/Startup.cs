using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using RestEase.HttpClientFactory;
using SFA.DAS.ApprenticeProgress.Functions.Api;
using SFA.DAS.ApprenticeProgress.Functions.Infrastructure;
using SFA.DAS.ApprenticeProgress.Functions.Services;
using SFA.DAS.Http.Configuration;
using SFA.DAS.NServiceBus.Configuration;
using SFA.DAS.NServiceBus.Extensions;
using SFA.DAS.PushNotifications.Messages.Commands;

[assembly: FunctionsStartup(typeof(SFA.DAS.ApprenticeProgress.Functions.Startup))]

namespace SFA.DAS.ApprenticeProgress.Functions
{
    [ExcludeFromCodeCoverage]
    public class Startup : FunctionsStartup
    {
        public IConfiguration? Configuration { get; set; }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            builder.ConfigureConfiguration();
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            Configuration = builder.GetContext().Configuration;
            var useManagedIdentity = !Configuration.IsLocalAcceptanceOrDev();

            builder.Services.AddLogging();
            builder.Services.AddApplicationInsightsTelemetry();
            builder.Services.AddApplicationOptions();
            builder.Services.ConfigureFromOptions(f => f.ApprenticeProgressApi);
            builder.Services.AddSingleton<IMessageService, MessageService>();
            builder.Services.AddSingleton<IApimClientConfiguration>(x => x.GetRequiredService<ApprenticeProgressApiOptions>());

            builder.UseNServiceBus((IConfiguration appConfiguration) =>
            {

                var configuration = new ServiceBusTriggeredEndpointConfiguration(QueueNames.PushNotificationsQueue);

                //var configuration = ServiceBusEndpointFactory.CreateSingleQueueConfiguration(QueueNames.PushNotificationsQueue, appConfiguration, useManagedIdentity);
                configuration.AdvancedConfiguration.UseNewtonsoftJsonSerializer();
                configuration.AdvancedConfiguration.EnableInstallers();
                configuration.Transport.Routing().RouteToEndpoint(typeof(ProcessMessageCommand), QueueNames.PushNotificationsQueue);

                var endpointConfiguration = new EndpointConfiguration(QueueNames.PushNotificationsQueue)
                    .UseErrorQueue($"{QueueNames.PushNotificationsQueue}-errors")
                    .UseInstallers()
                    .UseNewtonsoftJsonSerializer();

                if (!string.IsNullOrEmpty(Configuration["NServiceBusConnectionString"]))
                {
                    endpointConfiguration.UseLicense(Configuration["NServiceBusLicense"]);
                }

                endpointConfiguration.SendOnly();

                if (Configuration["NServiceBusConnectionString"].Equals("UseLearningEndpoint=true", StringComparison.CurrentCultureIgnoreCase))
                {
                    var transport = endpointConfiguration.UseLearningTransport();
                }
                else
                {
                    var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
                    transport.ConnectionString(Configuration["NServiceBusConnectionString"]);
                    transport.AddRouting(routeSettings =>
                    {
                        routeSettings.RouteToEndpoint(typeof(ProcessMessageCommand), QueueNames.PushNotificationsQueue);
                    });
                }

                var endpoint = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

                builder.Services.AddSingleton(p => endpoint)
                    .AddSingleton<IMessageSession>(p => p.GetService<IEndpointInstance>());
                   
                return configuration;
            });

            builder.Services.AddSingleton<IApimClientConfiguration>(x => x.GetRequiredService<ApprenticeProgressApiOptions>());
            builder.Services.AddTransient<Http.MessageHandlers.DefaultHeadersHandler>();
            builder.Services.AddTransient<Http.MessageHandlers.LoggingMessageHandler>();
            builder.Services.AddTransient<Http.MessageHandlers.ApimHeadersHandler>();

            var url = builder.Services
                .BuildServiceProvider()
                .GetRequiredService<ApprenticeProgressApiOptions>()
                .ApiBaseUrl;

            builder.Services.AddRestEaseClient<IApprenticeProgressApi>(url)
                .AddHttpMessageHandler<Http.MessageHandlers.DefaultHeadersHandler>()
                .AddHttpMessageHandler<Http.MessageHandlers.ApimHeadersHandler>()
                .AddHttpMessageHandler<Http.MessageHandlers.LoggingMessageHandler>();
        }
    }
}