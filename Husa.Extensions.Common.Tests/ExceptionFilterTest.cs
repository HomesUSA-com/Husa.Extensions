namespace Husa.Extensions.Common.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
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

    public class ExceptionFilterTest
    {
        private readonly Mock<ILogger<ExceptionLoggingFilterAttribute>> logger = new();

        private interface IFakeEntity
        {
        }

        [Fact]
        public async Task NotFoundExceptionThrownIsHandledCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var exceptionContext = GetExceptionContext();
            exceptionContext.SetupAllProperties();
            exceptionContext.SetupGet(ec => ec.Exception).Returns(new NotFoundException<IFakeEntity>(id));

            var sut = new ExceptionLoggingFilterAttribute(this.logger.Object);

            // Act
            await sut.OnExceptionAsync(exceptionContext.Object);

            // Assert
            Assert.True(exceptionContext.Object.ExceptionHandled);
            var notFoundResult = Assert.IsAssignableFrom<NotFoundObjectResult>(exceptionContext.Object.Result);
            var apiError = Assert.IsType<ApiError>(notFoundResult.Value);
            Assert.Contains(id.ToString(), apiError.Message);
        }

        [Fact]
        public async Task ExceptionLoggingFilterTets_RequestExceptionAsync_ExceptionHandledSuccess()
        {
            // Arrange
            const string stackTrace = "Test stacktrace";
            const string exceptionMessage = "someMethod received a null argument!";

            var mockException = new Mock<Exception>();
            mockException.Setup(e => e.StackTrace).Returns(stackTrace).Verifiable();
            mockException.Setup(e => e.Message).Returns(exceptionMessage).Verifiable();

            var exceptionContext = GetExceptionContext();
            exceptionContext.SetupAllProperties();

            exceptionContext.Setup(x => x.Exception).Returns(mockException.Object).Verifiable();
            exceptionContext.Setup(x => x.Exception.GetBaseException()).Returns(mockException.Object).Verifiable();
            var sut = new ExceptionLoggingFilterAttribute(this.logger.Object);

            // Act
            await sut.OnExceptionAsync(exceptionContext.Object);

            // Assert
            mockException.Verify();
            var jsonResult = Assert.IsAssignableFrom<JsonResult>(exceptionContext.Object.Result);
            var apiError = Assert.IsAssignableFrom<ApiError>(jsonResult.Value);
            var statusCode = Assert.IsAssignableFrom<int>(exceptionContext.Object.HttpContext.Response.StatusCode);

            Assert.Equal(stackTrace, apiError.Detail);
            Assert.Equal(exceptionMessage, apiError.Message);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        private static Mock<ExceptionContext> GetExceptionContext()
        {
            var httpContext = new DefaultHttpContext();
            var routeData = new Mock<RouteData>();
            var actionDescriptor = new Mock<ActionDescriptor>();
            var actionContext = new Mock<ActionContext>(httpContext, routeData.Object, actionDescriptor.Object);
            var filters = new Mock<IList<IFilterMetadata>>();

            return new Mock<ExceptionContext>(actionContext.Object, filters.Object);
        }
    }
}
