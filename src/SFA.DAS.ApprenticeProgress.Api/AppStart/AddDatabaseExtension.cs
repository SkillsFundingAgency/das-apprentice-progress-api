using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.ApprenticeProgress.Data;

namespace SFA.DAS.ApprenticeProgress.Api.AppStart
{
    [ExcludeFromCodeCoverage]
    public static class AddDatabaseExtension
    {
        public static void AddDatabaseRegistration(this IServiceCollection services, IConfiguration config, string environmentName)
        {
            if (environmentName.Equals("DEV", StringComparison.CurrentCultureIgnoreCase))
            {
                services.AddDbContext<ApprenticeProgressDataContext>(options => options.UseInMemoryDatabase("SFA.DAS.ApprenticeProgress"), ServiceLifetime.Transient);
            }
            else if (environmentName.Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase))
            {
                services.AddDbContext<ApprenticeProgressDataContext>(options => options.UseSqlServer(config["ApplicationSettings:ConnectionString"]),ServiceLifetime.Transient);
            }
            else
            {
                services.AddSingleton(new AzureServiceTokenProvider());
                services.AddDbContext<ApprenticeProgressDataContext>(ServiceLifetime.Transient);    
            }
            
            services.AddTransient<IApprenticeProgressDataContext, ApprenticeProgressDataContext>(provider => provider.GetService<ApprenticeProgressDataContext>());
            services.AddTransient(provider => new Lazy<ApprenticeProgressDataContext>(provider.GetService<ApprenticeProgressDataContext>()));   
        }
    }
}