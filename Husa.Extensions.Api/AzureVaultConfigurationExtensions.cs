namespace Husa.Extensions.Api
{
    using System;
    using Azure.Extensions.AspNetCore.Configuration.Secrets;
    using Azure.Identity;
    using Azure.Security.KeyVault.Secrets;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    public static class AzureVaultConfigurationExtensions
    {
        public static IConfigurationBuilder AddKeyVault(this IConfigurationBuilder configuration, HostBuilderContext host)
        {
            if (host.HostingEnvironment.IsProduction())
            {
                var builtConfig = configuration.Build();
                var secretClient = new SecretClient(
                    new Uri($"https://{builtConfig["KeyVaultName"]}.vault.azure.net/"),
                    new DefaultAzureCredential());

                configuration.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
            }

            return configuration;
        }
    }
}
