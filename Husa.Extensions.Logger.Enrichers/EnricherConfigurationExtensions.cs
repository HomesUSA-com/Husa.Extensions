namespace Husa.Extensions.Logger.Enrichers
{
    using System;
    using Serilog;
    using Serilog.Configuration;

    public static class EnricherConfigurationExtensions
    {
        public static LoggerConfiguration WithCorrelationIdHeader(
            this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null)
            {
                throw new ArgumentNullException(nameof(enrichmentConfiguration));
            }

            return enrichmentConfiguration.With<CorrelationIdHeaderEnricher>();
        }
    }
}
