namespace Husa.Extensions.Api
{
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public static class LoggingConfiguration
    {
        public static ILogger GetLogger(IConfiguration configuration, IWebHostEnvironment env)
        {
            var loggerConfiguration = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration);

            if (env.IsProduction())
            {
                var telemetryConfiguration = new TelemetryConfiguration(configuration["Application:Monitoring:AzureApplicationInsightsInstrumentationKey"]);
                loggerConfiguration.WriteTo.ApplicationInsights(telemetryConfiguration, TelemetryConverter.Traces);
            }

            Log.Logger = loggerConfiguration.CreateLogger();
            return Log.Logger;
        }
    }
}
