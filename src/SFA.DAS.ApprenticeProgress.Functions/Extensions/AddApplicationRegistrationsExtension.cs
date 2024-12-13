using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.ApprenticeProgress.Functions.Services;

namespace SFA.DAS.ApprenticeProgress.Functions.Extensions;

[ExcludeFromCodeCoverage]
public static class AddApplicationRegistrationsExtension
{
    public static IServiceCollection AddApplicationRegistrations(this IServiceCollection services)
    {
        services.AddTransient<IMessageService, MessageService>();
        return services;
    }
}