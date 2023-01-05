namespace Husa.Extensions.Logger.Enrichers.Tests
{
    using System;
    using Husa.Extensions.Logger.Enrichers.Tests.Support;
    using Serilog;
    using Serilog.Configuration;
    using Xunit;

    public class EnricherConfigurationExtensionsTests
    {
        [Fact]
        public void WithCorrelationIdHeader_ThenLoggerIsCalled_ShouldNotThrowException()
        {
            // Arrange

            // Act
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Sink(new DelegateSink.DelegatingSink(e => { }))
                .Enrich.WithCorrelationIdHeader().CreateBootstrapLogger();
            var exception = Record.Exception(() => Log.Information("LOG"));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void WithCorrelationIdHeader_WhenLoggerEnrichmentConfigurationIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            LoggerEnrichmentConfiguration configuration = null;

            // Assert
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => configuration.WithCorrelationIdHeader());
        }
    }
}
