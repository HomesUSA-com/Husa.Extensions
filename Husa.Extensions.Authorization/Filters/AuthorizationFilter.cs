namespace Husa.Extensions.Authorization.Filters
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Husa.Extensions.Authorization.Enums;
    using Husa.Extensions.Authorization.Extensions;
    using Husa.Extensions.Common.Enums;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;

    public abstract class AuthorizationFilter : Interfaces.IAuthorizationFilter
    {
        public const string CurrentCompanyHeaderName = "CurrentCompanySelected";
        public const string CurrentMarketHeaderName = "CurrentMarketSelected";
        public const string CurrentEmployeeRoleHeaderName = "CurrentEmployeeRole";
        public const string UserTimeZoneHeaderName = "X-User-TimeZone";
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

            if (context.HttpContext.User == null || context.HttpContext.User.Identity == null || !context.HttpContext.User.Identity.IsAuthenticated)
            {
                var principal = this.GetClaimsPrincipalFromToken(context.HttpContext);
                if (principal != null)
                {
                    context.HttpContext.User = principal;
                }
            }

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

            if (context.HttpContext.Request.Headers.TryGetValue(UserTimeZoneHeaderName, out var timeZoneId))
            {
                user.TimeZoneId = timeZoneId;
            }

            this.userProvider.SetCurrentUser(user);
        }

        public abstract Task GetCompanyEmployeeAsync(AuthorizationFilterContext context, IUserContext user);

        protected ClaimsPrincipal GetClaimsPrincipalFromToken(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.TryGetValue("Authorization", out var authHeaderValues))
            {
                var authHeader = authHeaderValues.FirstOrDefault();
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    var token = authHeader.Substring("Bearer ".Length).Trim();
                    var tokenHandler = new JwtSecurityTokenHandler();
                    try
                    {
                        var jwtToken = tokenHandler.ReadJwtToken(token);
                        var identity = new ClaimsIdentity(jwtToken.Claims, "Custom");
                        return new ClaimsPrincipal(identity);
                    }
                    catch (Exception ex)
                    {
                        this.logger.LogError($"Error al leer el token: {ex.Message}");
                        return null;
                    }
                }
            }

            return null;
        }
    }
}
