using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestEase.HttpClientFactory;
using SFA.DAS.Api.Common.Infrastructure;
using SFA.DAS.ApprenticeProgress.Functions.Api.Clients;
using SFA.DAS.ApprenticeProgress.Functions.Authentication;
using SFA.DAS.ApprenticeProgress.Functions.Configuration;

namespace SFA.DAS.ApprenticeProgress.Functions.HttpClientConfiguration
{
    [ExcludeFromCodeCoverage]
    public static class HttpClientConfigurationExtension
    {
        public static IServiceCollection ConfigureHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();

            AddApprenticeProgressApiClient(services, configuration);

            return services;
        }

        private static void AddApprenticeProgressApiClient(IServiceCollection services, IConfiguration configuration)
        {
            var apiConfig = configuration.GetSection(nameof(ApplicationConfiguration)).Get<ApplicationConfiguration>().ApprenticeProgressApiConfiguration;

            services.AddRestEaseClient<IApprenticeProgressApiClient>(apiConfig.Url)
               .AddHttpMessageHandler(() => new InnerApiAuthenticationHeaderHandler(new AzureClientCredentialHelper(), apiConfig.Identifier));
        }
    }
}
