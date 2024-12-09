namespace Husa.Extensions.ServiceBus.Services
{
    using System;
    using Husa.Extensions.ServiceBus.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class TraceIdProvider : IProvideTraceId, IConfigureTraceId
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<TraceIdProvider> logger;

        private string traceId;

        public TraceIdProvider(IHttpContextAccessor httpContextAccessor, ILogger<TraceIdProvider> logger)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string TraceId
        {
            get
            {
                if (string.IsNullOrEmpty(this.traceId))
                {
                    throw new InvalidOperationException($"The {nameof(this.traceId)} must be set before using it");
                }

                return this.traceId;
            }
        }

        public void SetTraceId(string traceId = null)
        {
            var contextTraceId = this.httpContextAccessor.HttpContext?.TraceIdentifier;
            if (!string.IsNullOrEmpty(contextTraceId))
            {
                this.logger.LogInformation("Setting trace Id {traceId} found in the http context", contextTraceId);
                this.traceId = contextTraceId;
                return;
            }

            if (!string.IsNullOrEmpty(traceId))
            {
                this.logger.LogInformation("Setting trace Id {traceId} value passed as parameter", traceId);
                this.traceId = traceId;
                return;
            }

            this.traceId = Guid.NewGuid().ToString();
        }
    }
}
