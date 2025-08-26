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
        public static IConfigurationBuilder AddKeyVault(this IConfigurationBuilder configuration)
        {
            var builtConfig = configuration.Build();
            string assignedClientId = builtConfig["KeyVault:ClientId"];
            var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = assignedClientId });
            var secretClient = new SecretClient(
                new Uri($"https://{builtConfig["KeyVault:Name"]}.vault.azure.net/"),
                credential);
            configuration.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());

            return configuration;
        }

        public static IConfigurationBuilder AddKeyVault(this IConfigurationBuilder configuration, HostBuilderContext host)
            => host.HostingEnvironment.IsProduction() ? configuration.AddKeyVault() : configuration;
    }
}
