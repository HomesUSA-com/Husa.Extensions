namespace Husa.Extensions.Authorization.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Husa.Extensions.Authorization.Enums;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class RolesFilterAttribute : RoleAuthorizeAttribute
    {
        private readonly IEnumerable<UserRole> userRoles;
        private readonly IEnumerable<RoleEmployee> employeeRoles;

        public RolesFilterAttribute(UserRole[] userRoles = null, RoleEmployee[] employeeRoles = null)
        {
            this.employeeRoles = employeeRoles ?? [];
            this.userRoles = userRoles ?? [];
        }

        protected override Task<bool> AuthorizeAsync(ActionExecutingContext context)
        {
            var userContext = context.HttpContext.RequestServices.GetRequiredService<IUserContextProvider>();
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ApiAuthorizationAttribute>>();

            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
            {
                logger.LogInformation("Skipping the filter and continue as Anonymous request");
                return Task.FromResult(true);
            }

            var user = userContext.GetCurrentUser();
            logger.LogInformation("Validating API authorization for user '{userId}'.", user.Id);

            var allowedUserRole = !this.userRoles.Any() || this.userRoles.Contains(user.UserRole);
            var allowedEmployeeRole = user.UserRole != UserRole.User
                || !this.employeeRoles.Any()
                || (user.EmployeeRole.HasValue && this.employeeRoles.Contains(user.EmployeeRole.Value));

            return Task.FromResult(allowedUserRole && allowedEmployeeRole);
        }
    }
}
