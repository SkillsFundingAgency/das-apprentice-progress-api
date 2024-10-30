﻿using Microsoft.Extensions.Configuration;
using SFA.DAS.Configuration.AzureTableStorage;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace SFA.DAS.ApprenticeProgress.Functions.Extensions;

[ExcludeFromCodeCoverage]
public static class AddConfigurationExtension
{
    public static void AddConfiguration(this IConfigurationBuilder builder)
    {
        builder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("local.settings.json", optional: true);

        var config = builder.Build();

        builder.AddAzureTableStorage(options =>
        {
            options.ConfigurationKeys = config["ConfigNames"].Split(",");
            options.StorageConnectionString = config["ConfigurationStorageConnectionString"];
            options.EnvironmentName = config["EnvironmentName"];
            options.PreFixConfigurationKeys = false;
        });
    }
}