using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SFA.DAS.ApprenticeProgress.Data;
using SFA.DAS.ApprenticeProgress.Functions.Configuration;
using SFA.DAS.ApprenticeProgress.Functions.Extensions;
using SFA.DAS.ApprenticeProgress.Functions.HttpClientConfiguration;

namespace SFA.DAS.ApprenticeProgress.Functions;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static async Task Main(string[] args)
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddConfiguration();
            })
            .ConfigureServices((context, services) =>
            {
                services
                    .AddOptions()
                    .Configure<ApplicationConfiguration>(
                        context.Configuration.GetSection(
                            nameof(ApplicationConfiguration)))
                    .ConfigureHttpClients(context.Configuration)
                    .AddApplicationRegistrations()
                    .AddNServiceBus(context.Configuration);

                services.AddDbContext<ApprenticeProgressDataContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString(
                            "SqlConnectionString")));

                services.AddScoped<IApprenticeProgressDataContext>(sp =>
                    sp.GetRequiredService<ApprenticeProgressDataContext>());
            })
            .Build();

        await host.RunAsync();
    }
}
