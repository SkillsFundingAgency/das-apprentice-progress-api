using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Contentful.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestEase.HttpClientFactory;
using SFA.DAS.Api.Common.Infrastructure;
using SFA.DAS.ApprenticeProgress.Functions.Api.Clients;
using SFA.DAS.ApprenticeProgress.Functions.Authentication;
using SFA.DAS.ApprenticeProgress.Functions.Configuration;
using SFA.DAS.ApprenticeProgress.Infrastructure.Contentful;

namespace SFA.DAS.ApprenticeProgress.Functions.HttpClientConfiguration
{
    [ExcludeFromCodeCoverage]
    public static class HttpClientConfigurationExtension
    {
        public static IServiceCollection ConfigureHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();

            AddApprenticeProgressApiClient(services, configuration);
            AddContentfulClient(services, configuration);

            return services;
        }

        private static void AddApprenticeProgressApiClient(IServiceCollection services, IConfiguration configuration)
        {
            //var apiConfig = configuration.GetSection(nameof(ApplicationConfiguration)).Get<ApplicationConfiguration>().ApprenticeProgressApiConfiguration;

            var apiConfig = configuration
                .GetSection(nameof(ApprenticeProgressApiConfiguration))
                .Get<ApprenticeProgressApiConfiguration>();

            services.AddRestEaseClient<IApprenticeProgressApiClient>(apiConfig.Url)
               .AddHttpMessageHandler(() => new InnerApiAuthenticationHeaderHandler(new AzureClientCredentialHelper(), apiConfig.Identifier));
        }

        private static void AddContentfulClient(
    IServiceCollection services,
    IConfiguration configuration)
        {
            var contentfulConfig = configuration
                .GetSection("Contentful")
                .Get<ContentfulOptions>();

            services.AddSingleton(sp =>
            {
                var httpClientFactory =
                    sp.GetRequiredService<IHttpClientFactory>();

                var httpClient = httpClientFactory.CreateClient();

                var options =
                    new Contentful.Core.Configuration.ContentfulOptions
                    {
                        DeliveryApiKey = contentfulConfig.DeliveryApiKey,
                        SpaceId = contentfulConfig.SpaceId,
                        Environment = contentfulConfig.Environment
                    };

                return new ContentfulClient(httpClient, options);
            });
        }
    }
}
