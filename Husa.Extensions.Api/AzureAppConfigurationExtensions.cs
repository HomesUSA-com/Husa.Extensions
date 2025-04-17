namespace Husa.Extensions.Api
{
    using System;
    using Azure.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    public static class AzureAppConfigurationExtensions
    {
        public static IConfigurationBuilder AddAppConfiguration(this IConfigurationBuilder configuration, HostBuilderContext host, string appPrefix)
        {
            if (host.HostingEnvironment.IsProduction())
            {
                string azureAppConfigEndpoint = Environment.GetEnvironmentVariable("AZURE_APPCONFIGURATION_ENDPOINT");
                if (!string.IsNullOrEmpty(azureAppConfigEndpoint))
                {
                    configuration.AddAzureAppConfiguration(options =>
                    {
                        options
                            .Connect(new Uri(azureAppConfigEndpoint), new DefaultAzureCredential())
                            .Select($"{appPrefix}:*")
                            .TrimKeyPrefix($"{appPrefix}:")
                            .ConfigureRefresh(refreshOptions =>
                            {
                                // Trigger full configuration refresh only if the `Sentinel` changes.
                                refreshOptions.Register($"{appPrefix}:Sentinel", refreshAll: true);
                            })
                            .ConfigureKeyVault(kvOptions =>
                            {
                                kvOptions.SetCredential(new DefaultAzureCredential());
                            });
                    });
                }
            }

            return configuration;
        }
    }
}
