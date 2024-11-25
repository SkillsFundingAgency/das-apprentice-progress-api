﻿using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.ApprenticeProgress.Functions.Configuration;

[ExcludeFromCodeCoverage]
public class NServiceBusConfiguration
{
    public string NServiceBusConnectionString { get; set; }

    public string NServiceBusLicense
    {
        get => _nServiceBusLicense;
        set => _nServiceBusLicense = System.Net.WebUtility.HtmlDecode(value);
    }

    private string _nServiceBusLicense;
}