namespace Husa.Extensions.Authorization.Tests.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Husa.Extensions.Authorization.Enums;
    using Husa.Extensions.Authorization.Models;
    using Husa.Extensions.Authorization.Tests.Providers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Xunit;

    public class RolesFilterAttributeTests
    {
        [Fact]
        public async Task AuthorizeAsync_UserHasValidUserRole_ReturnsTrue()
        {
            // Arrange
            var userRoles = new[] { UserRole.MLSAdministrator };
            var attribute = new TestableRolesFilterAttribute(userRoles);

            var actionExecutingContext = this.GetActionExecutingContext(new UserContext()
            {
                Id = Guid.NewGuid(),
                UserRole = UserRole.MLSAdministrator,
                IsMLSAdministrator = true,
            });

            // Act
            var result = await attribute.AuthorizeAsync(actionExecutingContext);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AuthorizeAsync_UserHasInvalidUserRole_ReturnsFalse()
        {
            // Arrange
            var userRoles = new[] { UserRole.MLSAdministrator };
            var attribute = new TestableRolesFilterAttribute(userRoles);

            var actionExecutingContext = this.GetActionExecutingContext(new UserContext()
            {
                Id = Guid.NewGuid(),
                UserRole = UserRole.User,
                IsMLSAdministrator = false,
            });

            // Act
            var result = await attribute.AuthorizeAsync(actionExecutingContext);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task AuthorizeAsync_UserHasValidEmployeeRole_ReturnsTrue()
        {
            // Arrange
            var employeeRoles = new[] { RoleEmployee.SalesEmployee };
            var attribute = new TestableRolesFilterAttribute(employeeRoles: employeeRoles);

            var actionExecutingContext = this.GetActionExecutingContext(new UserContext()
            {
                Id = Guid.NewGuid(),
                UserRole = UserRole.User,
                EmployeeRole = RoleEmployee.SalesEmployee,
                IsMLSAdministrator = false,
            });

            // Act
            var result = await attribute.AuthorizeAsync(actionExecutingContext);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AuthorizeAsync_UserIsAnonymous_ReturnsTrue()
        {
            // Arrange
            var attribute = new TestableRolesFilterAttribute();
            var userContextProvider = new Mock<IUserContextProvider>();

            var serviceProvider = new ServiceCollection()
                .AddSingleton(userContextProvider.Object)
                .AddLogging()
                .BuildServiceProvider();

            var httpContext = new DefaultHttpContext { RequestServices = serviceProvider };

            var actionDescriptor = new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor
            {
                EndpointMetadata = new List<object> { new Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute() },
            };

            var actionContext = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), actionDescriptor);
            var actionExecutingContext = new ActionExecutingContext(actionContext, new List<IFilterMetadata>(), new Dictionary<string, object>(), new object());

            // Act
            var result = await attribute.AuthorizeAsync(actionExecutingContext);

            // Assert
            Assert.True(result);
        }

        private ActionExecutingContext GetActionExecutingContext(UserContext user)
        {
            var userContextProvider = new Mock<IUserContextProvider>();
            userContextProvider.Setup(x => x.GetCurrentUser()).Returns(user);

            var serviceProvider = new ServiceCollection()
                .AddSingleton(userContextProvider.Object)
                .AddLogging()
                .BuildServiceProvider();

            var httpContext = new DefaultHttpContext { RequestServices = serviceProvider };
            var actionDescriptor = new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor
            {
                EndpointMetadata = new List<object>(),
            };
            var actionContext = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), actionDescriptor);
            return new ActionExecutingContext(actionContext, new List<IFilterMetadata>(), new Dictionary<string, object>(), new object());
        }
    }
}
