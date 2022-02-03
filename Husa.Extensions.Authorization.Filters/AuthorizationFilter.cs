namespace Husa.Extensions.Authorization.Filters
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Husa.CompanyServicesManager.Api.Client.Interfaces;
    using Husa.Extensions.Authorization.Enums;
    using Husa.Extensions.Authorization.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;

    public class AuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly ILogger<AuthorizationFilter> logger;
        private readonly IUserProvider userProvider;
        private readonly IServiceSubscriptionClient serviceSubscriptionClient;

        public AuthorizationFilter(IUserProvider userProvider, IServiceSubscriptionClient serviceSubscriptionClient, ILogger<AuthorizationFilter> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
            this.serviceSubscriptionClient = serviceSubscriptionClient ?? throw new ArgumentNullException(nameof(serviceSubscriptionClient));
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
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

            user.CompanyId = Guid.TryParse(context.HttpContext.Request.Headers["CurrentCompanySelected"], out var currentCompanyId) &&
                !currentCompanyId.Equals(Guid.Empty) ? currentCompanyId : null;

            if (!user.IsMLSAdministrator)
            {
                if (!user.CompanyId.HasValue)
                {
                    this.logger.LogError("CompanyId isn't provided");
                    context.Result = new UnauthorizedObjectResult("CompanyId isn't provided");
                    return;
                }

                var userEmployee = await this.serviceSubscriptionClient.Employee.GetEmployeeByUserAndCompany(user.Id, user.CompanyId.Value);

                if (userEmployee is null)
                {
                    this.logger.LogError($"The user with id: '{user.Id}' doesn't exist.");
                    context.Result = new UnauthorizedObjectResult($"The user with id:'{user.Id}' doesn't exist.");
                    return;
                }

                user.EmployeeRole = (RoleEmployee)userEmployee.RoleName;
            }

            this.userProvider.SetCurrentUser(user);
        }
    }
}
