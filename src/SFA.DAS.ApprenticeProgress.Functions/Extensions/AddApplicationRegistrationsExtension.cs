using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.ApprenticeProgress.Functions.Services;
using System.Diagnostics.CodeAnalysis;

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