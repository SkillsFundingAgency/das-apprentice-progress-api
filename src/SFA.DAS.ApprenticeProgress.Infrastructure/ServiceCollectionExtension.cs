using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.ApprenticeProgress.Application.Interfaces;
using SFA.DAS.ApprenticeProgress.Infrastructure.Contentful;

namespace SFA.DAS.ApprenticeProgress.Infrastructure;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IContentfulService, ContentfulService>();

        return services;
    }
}
