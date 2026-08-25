using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SFA.DAS.ApprenticeProgress.Data;
using SFA.DAS.ApprenticeProgress.Functions.Configuration;
using SFA.DAS.ApprenticeProgress.Functions.Extensions;
using SFA.DAS.ApprenticeProgress.Functions.HttpClientConfiguration;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration(
        builder =>
        {
            builder.AddConfiguration();            
        })
    .ConfigureServices((context, s) =>
    {
        s
            .AddOptions()
            .Configure<ApplicationConfiguration>(context.Configuration.GetSection(nameof(ApplicationConfiguration)))            
            .ConfigureHttpClients(context.Configuration)
            .AddApplicationRegistrations()            
            .AddNServiceBus(context.Configuration);

        s.AddDbContext<ApprenticeProgressDataContext>(options =>
        options.UseSqlServer(context.Configuration.GetConnectionString("SqlConnectionString")));

        s.AddScoped<IApprenticeProgressDataContext>(sp =>
        sp.GetRequiredService<ApprenticeProgressDataContext>());
    })
    .Build();

await host.RunAsync();
