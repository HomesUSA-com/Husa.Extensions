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
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<RolesFilterAttribute>>();

            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
            {
                logger.LogInformation("Skipping the filter and continue as Anonymous request");
                return Task.FromResult(true);
            }

            var user = userContext.GetCurrentUser();
            logger.LogInformation("Validating API authorization for user '{@User}'.", user);

            if (user.IsMLSAdministrator && user.EmployeeRole.HasValue)
            {
                return Task.FromResult(this.IsAllowedUserRole(UserRole.User) && this.IsAllowedEmployeeRole(user.EmployeeRole));
            }

            var allowedUserRole = this.IsAllowedUserRole(user.UserRole);
            var allowedEmployeeRole = user.UserRole != UserRole.User || this.IsAllowedEmployeeRole(user.EmployeeRole);

            return Task.FromResult(allowedUserRole && allowedEmployeeRole);
        }

        private bool IsAllowedEmployeeRole(RoleEmployee? employeeRole)
        {
            return !this.employeeRoles.Any()
                || (employeeRole.HasValue && this.employeeRoles.Contains(employeeRole.Value));
        }

        private bool IsAllowedUserRole(UserRole userRole)
        {
            return !this.userRoles.Any() || this.userRoles.Contains(userRole);
        }
    }
}
