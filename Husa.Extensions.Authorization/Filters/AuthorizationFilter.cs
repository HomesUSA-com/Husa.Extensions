namespace Husa.Extensions.Authorization.Filters
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Husa.Extensions.Authorization.Enums;
    using Husa.Extensions.Authorization.Extensions;
    using Husa.Extensions.Common.Enums;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;

    public abstract class AuthorizationFilter : Interfaces.IAuthorizationFilter
    {
        public const string CurrentCompanyHeaderName = "CurrentCompanySelected";
        public const string CurrentMarketHeaderName = "CurrentMarketSelected";
        public const string CurrentEmployeeRoleHeaderName = "CurrentEmployeeRole";
        protected readonly ILogger<AuthorizationFilter> logger;
        protected readonly IUserProvider userProvider;

        protected AuthorizationFilter(IUserProvider userProvider, ILogger<AuthorizationFilter> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        public virtual async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
            {
                this.logger.LogInformation("Skipping the filter and continue as Anonymous request");
                return;
            }

            this.logger.LogInformation("Authorizing user request to allow access to the application.");

            var user = await Task.FromResult(context.HttpContext.User.GetUserContext());

            if (user == null)
            {
                this.logger.LogError($"The user in token doesn't exist.");
                context.Result = new UnauthorizedObjectResult($"The user in token doesn't exist.");
                return;
            }

            var marketHeader = context.HttpContext.Request.Headers[CurrentMarketHeaderName];
            user.Market = Enum.TryParse(marketHeader.FirstOrDefault(), result: out MarketCode marketCode, ignoreCase: true) ? marketCode : null;

            user.CompanyId = Guid.TryParse(context.HttpContext.Request.Headers[CurrentCompanyHeaderName], out var currentCompanyId) &&
                !currentCompanyId.Equals(Guid.Empty) ? currentCompanyId : null;

            if (user.IsMLSAdministrator)
            {
                user.EmployeeRole = Enum.TryParse(context.HttpContext.Request.Headers[CurrentEmployeeRoleHeaderName], out RoleEmployee employeeRole) ? employeeRole : null;
            }

            await this.GetCompanyEmployeeAsync(context, user);

            this.userProvider.SetCurrentUser(user);
        }

        public abstract Task GetCompanyEmployeeAsync(AuthorizationFilterContext context, IUserContext user);
    }
}
