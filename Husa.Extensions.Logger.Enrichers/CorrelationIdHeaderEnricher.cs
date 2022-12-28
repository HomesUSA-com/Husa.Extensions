namespace Husa.Extensions.Logger.Enrichers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Serilog.Core;
    using Serilog.Events;

    public class CorrelationIdHeaderEnricher : ILogEventEnricher
    {
        public const string HeaderKey = "x-correlation-id";
        private const string CorrelationIdPropertyName = "CorrelationId";
        private readonly IHttpContextAccessor contextAccessor;

        public CorrelationIdHeaderEnricher()
            : this(new HttpContextAccessor())
        {
        }

        internal CorrelationIdHeaderEnricher(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (this.contextAccessor.HttpContext == null)
            {
                return;
            }

            var correlationId = this.GetCorrelationId();

            var correlationIdProperty = new LogEventProperty(CorrelationIdPropertyName, new ScalarValue(correlationId));

            logEvent.AddOrUpdateProperty(correlationIdProperty);
        }

        private string GetCorrelationId()
        {
            var header = this.GetHeader();

            var correlationId = string.IsNullOrEmpty(header)
                                    ? Guid.NewGuid().ToString()
                                    : header;

            this.contextAccessor.HttpContext.Response.Headers.TryAdd(HeaderKey, correlationId);
            this.contextAccessor.HttpContext.Request.Headers.TryAdd(HeaderKey, correlationId);

            return correlationId;
        }

        private string GetHeader()
        {
            if (this.contextAccessor.HttpContext.Request.Headers.TryGetValue(HeaderKey, out var values))
            {
                return values.FirstOrDefault();
            }

            if (this.contextAccessor.HttpContext.Response.Headers.TryGetValue(HeaderKey, out values))
            {
                return values.FirstOrDefault();
            }

            return string.Empty;
        }
    }
}
