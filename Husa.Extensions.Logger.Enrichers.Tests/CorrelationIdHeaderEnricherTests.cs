namespace Husa.Extensions.Logger.Enrichers.Tests
{
    using System;
    using Husa.Extensions.Logger.Enrichers.Tests.Support;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Serilog;
    using Serilog.Events;
    using Xunit;

    public class CorrelationIdHeaderEnricherTests
    {
        private const string HeaderKey = "x-correlation-id";
        private readonly Mock<IHttpContextAccessor> httpContextAccessor;
        private readonly CorrelationIdHeaderEnricher enricher;

        public CorrelationIdHeaderEnricherTests()
        {
            this.httpContextAccessor = new Mock<IHttpContextAccessor>();
            this.enricher = new CorrelationIdHeaderEnricher(this.httpContextAccessor.Object);
        }

        [Fact]
        public void When_CorrelationIdNotInHeader_Should_CreateCorrelationIdProperty()
        {
            // Arange
            this.httpContextAccessor.Setup(x => x.HttpContext).Returns(new DefaultHttpContext());
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .Enrich.With(this.enricher)
                .WriteTo.Sink(new DelegateSink.DelegatingSink(e => evt = e))
                .CreateLogger();

            // Act
            log.Information(@"Has a CorrelationId property");

            // Assert
            Assert.NotNull(evt);
            Assert.True(evt?.Properties.ContainsKey("CorrelationId"));
            Assert.NotNull(evt?.Properties["CorrelationId"]);
        }

        [Fact]
        public void When_CorrelationIdIsInHeader_Should_ExtractCorrelationIdFromHeader()
        {
            // Arange
            var correlationIdTest = Guid.NewGuid().ToString();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers[HeaderKey] = correlationIdTest;
            this.httpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .Enrich.With(this.enricher)
                .WriteTo.Sink(new DelegateSink.DelegatingSink(e => evt = e))
                .CreateLogger();

            // Act
            log.Information(@"Has a CorrelationId property");

            // Assert
            Assert.NotNull(evt);
            Assert.True(evt.Properties.ContainsKey("CorrelationId"));
            Assert.Equal(correlationIdTest, evt.Properties["CorrelationId"].ToString().Replace("\"", string.Empty));
        }

        [Fact]
        public void When_CurrentHttpContextIsNull_ShouldNot_CreateCorrelationIdProperty()
        {
            // Arange
            this.httpContextAccessor.Setup(x => x.HttpContext).Returns((Microsoft.AspNetCore.Http.HttpContext)null);
            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .Enrich.With(this.enricher)
                .WriteTo.Sink(new DelegateSink.DelegatingSink(e => evt = e))
                .CreateLogger();

            // Act
            log.Information(@"Does not have a CorrelationId property");

            // Assert
            Assert.NotNull(evt);
            Assert.False(evt.Properties.ContainsKey("CorrelationId"));
        }

        [Fact]
        public void When_MultipleLoggingCallsMade_Should_KeepUsingCreatedCorrelationIdProperty()
        {
            // Arange
            this.httpContextAccessor.Setup(x => x.HttpContext).Returns(new DefaultHttpContext());

            LogEvent evt = null;
            var log = new LoggerConfiguration()
                .Enrich.With(this.enricher)
                .WriteTo.Sink(new DelegateSink.DelegatingSink(e => evt = e))
                .CreateLogger();

            // Act
            log.Information(@"Has a CorrelationId property");
            var correlationId = evt.Properties["CorrelationId"].ToString().Replace("\"", string.Empty);
            log.Information(@"Here is another event");

            // Assert
            Assert.Equal(correlationId, evt.Properties["CorrelationId"].ToString().Replace("\"", string.Empty));
        }
    }
}
