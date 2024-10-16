namespace Husa.Extensions.Api.Configuration
{
    using System;
    using System.IO;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    public static class HostConfiguration
    {
        private const string ShareSettingsFileName = "sharedsettings.json";
        public static void ConfigureHost(this HostBuilderContext host, IConfigurationBuilder configuration, bool isFunction)
        {
            var basePath = Environment.CurrentDirectory;
            if (isFunction && !host.HostingEnvironment.IsDevelopment())
            {
                basePath = $"{Environment.GetEnvironmentVariable("HOME")}/site/wwwroot";
            }

            configuration.SetBasePath(basePath);
            configuration
                .AddSharedSettings(host, isFunction: isFunction)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            if (host.HostingEnvironment.IsDevelopment())
            {
                configuration.AddJsonFile($"appsettings.{host.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
            }

            configuration.AddEnvironmentVariables();

            if (!host.HostingEnvironment.IsDevelopment())
            {
                configuration.AddKeyVault(host);
            }
        }

        private static IConfigurationBuilder AddSharedSettings(this IConfigurationBuilder configuration, HostBuilderContext host, bool optional = false, bool reloadOnChange = true, bool isFunction = false)
        {
            var sharedSettingsPath = ShareSettingsFileName;
            if (!isFunction && host.HostingEnvironment.IsDevelopment())
            {
                var hostingDirectory = new DirectoryInfo(Environment.CurrentDirectory);
                sharedSettingsPath = Path.Combine(hostingDirectory.Parent.FullName, ShareSettingsFileName);
            }

            return configuration.AddJsonFile(sharedSettingsPath, optional, reloadOnChange);
        }
    }
}
