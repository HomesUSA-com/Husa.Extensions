namespace Husa.Extensions.Media.Extensions
{
    using Azure.Storage.Blobs;
    using Husa.Extensions.Media.Interfaces;
    using Husa.Extensions.Media.Models;
    using Husa.Extensions.Media.Services;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class MediaBootstrapper
    {
        public static IServiceCollection ConfigureAzureBlobConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBlobService, BlobService>();
            services.AddScoped(x =>
            {
                var azureConfig = configuration.GetSection(AzureSettings.Section).Get<AzureSettings>();
                var connectionString = string.Format(configuration.GetConnectionString("AzureBlobConnection"), azureConfig.AccountName, azureConfig.AccountKey);
                var blobContainerName = configuration.GetSection("Application").GetSection("AzureConfiguration").GetValue("BlobContainerName", string.Empty);
                var blobContainerClient = new BlobContainerClient(connectionString, blobContainerName);
                return blobContainerClient;
            });
            return services;
        }
    }
}
