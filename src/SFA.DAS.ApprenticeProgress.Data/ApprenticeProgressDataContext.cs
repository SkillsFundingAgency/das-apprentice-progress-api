using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SFA.DAS.ApprenticeProgress.Data.Configuration;
using SFA.DAS.ApprenticeProgress.Domain.Configuration;

namespace SFA.DAS.ApprenticeProgress.Data
{
    public interface IApprenticeProgressDataContext
    {
        DbSet<Domain.Entities.ApprenticeshipCategory> ApprenticeshipCategory { get; set; }
        DbSet<Domain.Entities.KSBProgress> KSBProgress { get; set; }
        DbSet<Domain.Entities.KSBProgressStatusHistory> KSBProgressStatusHistory { get; set; }
        DbSet<Domain.Entities.Task> Task { get; set; }
        DbSet<Domain.Entities.TaskCategory> TaskCategory { get; set; }
        DbSet<Domain.Entities.TaskFile> TaskFile { get; set; }
        DbSet<Domain.Entities.TaskKSBs> TaskKSBs { get; set; }
        DbSet<Domain.Entities.TaskReminder> TaskReminder { get; set; }

        int SaveChanges();
    }

    [ExcludeFromCodeCoverage]
    public partial class ApprenticeProgressDataContext : DbContext, IApprenticeProgressDataContext
    {
        private const string AzureResource = "https://database.windows.net/";

        public virtual DbSet<Domain.Entities.ApprenticeshipCategory> ApprenticeshipCategory { get; set; }
        public virtual DbSet<Domain.Entities.KSBProgress> KSBProgress { get; set; }
        public virtual DbSet<Domain.Entities.KSBProgressStatusHistory> KSBProgressStatusHistory { get; set; }
        public virtual DbSet<Domain.Entities.Task> Task { get; set; }
        public virtual DbSet<Domain.Entities.TaskCategory> TaskCategory { get; set; }
        public virtual DbSet<Domain.Entities.TaskFile> TaskFile { get; set; }
        public virtual DbSet<Domain.Entities.TaskKSBs> TaskKSBs { get; set; }
        public virtual DbSet<Domain.Entities.TaskReminder> TaskReminder { get; set; }

        private readonly ApprenticeProgressConfiguration _configuration;
        private readonly AzureServiceTokenProvider _azureServiceTokenProvider;
     
        public ApprenticeProgressDataContext()
        {
        }

        public ApprenticeProgressDataContext(DbContextOptions options) : base(options)
        {
            
        }
        public ApprenticeProgressDataContext(IOptions<ApprenticeProgressConfiguration> config, DbContextOptions options, AzureServiceTokenProvider azureServiceTokenProvider) :base(options)
        {
            _configuration = config.Value;
            _azureServiceTokenProvider = azureServiceTokenProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            
            if (_configuration == null || _azureServiceTokenProvider == null)
            {
                return;
            }
            
            var connection = new SqlConnection
            {
                ConnectionString = _configuration.ConnectionString,
                AccessToken = _azureServiceTokenProvider.GetAccessTokenAsync(AzureResource).Result,
            };
            
            optionsBuilder.UseSqlServer(connection,options=>
                options.EnableRetryOnFailure(
                    5,
                    TimeSpan.FromSeconds(20),
                    null
                ));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApprenticeshipCategory());
            modelBuilder.ApplyConfiguration(new KSBProgress());
            modelBuilder.ApplyConfiguration(new KSBProgressStatusHistory());
            modelBuilder.ApplyConfiguration(new Task());
            modelBuilder.ApplyConfiguration(new TaskCategory());
            modelBuilder.ApplyConfiguration(new TaskFile());
            modelBuilder.ApplyConfiguration(new TaskKSBs());
            modelBuilder.ApplyConfiguration(new TaskReminder());

            base.OnModelCreating(modelBuilder);
        }
    }
}