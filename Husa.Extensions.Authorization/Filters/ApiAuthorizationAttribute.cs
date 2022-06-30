namespace Husa.Extensions.Authorization.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Husa.Extensions.Authorization.Enums;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class ApiAuthorizationAttribute : RoleAuthorizeAttribute
    {
        private readonly IEnumerable<RoleEmployee> role;

        public ApiAuthorizationAttribute(params RoleEmployee[] roles)
        {
            this.role = roles;
        }

        protected override Task<bool> AuthorizeAsync(ActionExecutingContext context)
        {
            var userContext = context.HttpContext.RequestServices.GetRequiredService<IUserContextProvider>();
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ApiAuthorizationAttribute>>();

            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
            {
                logger.LogInformation("Skipping the filter and continue as Anonymous request");
                Task.FromResult(true);
            }

            var user = userContext.GetCurrentUser();
            logger.LogInformation("Validating API authorization for user '{userId}'.", user.Id);

            return Task.FromResult(user.UserRole != UserRole.User || (user.EmployeeRole.HasValue && this.role.Contains(user.EmployeeRole.Value)));
        }
    }
}
