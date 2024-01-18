namespace Husa.Extensions.Common.Tests
{
    using System;
    using System.Collections.Generic;
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
