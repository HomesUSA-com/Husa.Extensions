namespace Husa.Extensions.Authorization.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Husa.Extensions.Authorization.Enums;
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

            var user = userContext.GetCurrentUser();
            logger.LogInformation("Authorizing user '{userId}' roles", user.Id);

            return Task.FromResult(user.IsMLSAdministrator || (user.EmployeeRole.HasValue && this.role.Contains(user.EmployeeRole.Value)));
        }
    }
}
