namespace Husa.Extensions.Common.Tests.Exceptions
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Husa.Extensions.Common.Exceptions;
    using Husa.Extensions.Common.Exceptions.Filters;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    public class ExceptionLoggingFilterAttributeTests
    {
        private readonly Mock<ILogger<ExceptionLoggingFilterAttribute>> loggerMock;
        private readonly ExceptionLoggingFilterAttribute filter;
        private readonly ActionContext actionContext;

        public ExceptionLoggingFilterAttributeTests()
        {
            this.loggerMock = new Mock<ILogger<ExceptionLoggingFilterAttribute>>();
            this.filter = new ExceptionLoggingFilterAttribute(this.loggerMock.Object);

            this.actionContext = new ActionContext
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };
        }

        [Fact]
        public async Task OnExceptionAsync_WithNotFoundException_ReturnsNotFoundResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var exception = new NotFoundException<string>(id);
            var context = this.CreateExceptionContext(exception);

            // Act
            await this.filter.OnExceptionAsync(context);

            // Assert
            Assert.True(context.ExceptionHandled);
            var result = Assert.IsType<ObjectResult>(context.Result);
            var apiError = Assert.IsType<ApiError>(result.Value);
            Assert.Contains($"Id '{id}' wasn't found for the type", apiError.Message);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task OnExceptionAsync_WithHttpRequestExceptionNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var exception = new HttpRequestException("Resource not found", null, HttpStatusCode.NotFound);
            var context = this.CreateExceptionContext(exception);

            // Act
            await this.filter.OnExceptionAsync(context);

            // Assert
            Assert.True(context.ExceptionHandled);
            var result = Assert.IsType<ObjectResult>(context.Result);
            var apiError = Assert.IsType<ApiError>(result.Value);
            Assert.Equal("Resource not found", apiError.Message);
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Theory]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.NotImplemented)]
        public async Task OnExceptionAsync_WithHttpRequestException_ReturnsBadRequestResult(HttpStatusCode statusCode)
        {
            // Arrange
            var exception = new HttpRequestException("Bad request", null, statusCode);
            var context = this.CreateExceptionContext(exception);

            // Act
            await this.filter.OnExceptionAsync(context);

            // Assert
            Assert.True(context.ExceptionHandled);
            var result = Assert.IsType<ObjectResult>(context.Result);
            var apiError = Assert.IsType<ApiError>(result.Value);
            Assert.Equal("Bad request", apiError.Message);
            Assert.Equal((int)statusCode, result.StatusCode);
        }

        [Fact]
        public async Task OnExceptionAsync_WithDomainException_ReturnsBadRequestResult()
        {
            // Arrange
            var exception = new DomainException("Domain validation error");
            var context = this.CreateExceptionContext(exception);

            // Act
            await this.filter.OnExceptionAsync(context);

            // Assert
            Assert.True(context.ExceptionHandled);
            var result = Assert.IsType<ObjectResult>(context.Result);
            var apiError = Assert.IsType<ApiError>(result.Value);
            Assert.Equal("Domain validation error", apiError.Message);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task OnExceptionAsync_WithUnauthorizedAccessException_ReturnsForbidResult()
        {
            // Arrange
            var exception = new UnauthorizedAccessException("Unauthorized access");
            var context = this.CreateExceptionContext(exception);

            // Act
            await this.filter.OnExceptionAsync(context);

            // Assert
            Assert.True(context.ExceptionHandled);
            var result = Assert.IsType<ObjectResult>(context.Result);
            Assert.Equal((int)HttpStatusCode.Forbidden, result.StatusCode);
        }

        [Fact]
        public async Task OnExceptionAsync_WithUnhandledException_ReturnsInternalServerError()
        {
            // Arrange
            var exception = new Exception("Unhandled exception");
            var context = this.CreateExceptionContext(exception);

            // Act
            await this.filter.OnExceptionAsync(context);

            // Assert
            Assert.True(context.ExceptionHandled);
            var result = Assert.IsType<ObjectResult>(context.Result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public void Constructor_WithNullLogger_ThrowsArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new ExceptionLoggingFilterAttribute(null));
            Assert.Equal("logger", exception.ParamName);
        }

        private ExceptionContext CreateExceptionContext(Exception exception)
        => new(this.actionContext, [])
        {
            Exception = exception,
        };
    }
}
