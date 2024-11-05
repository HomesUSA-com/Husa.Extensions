namespace Husa.Extensions.Authorization.Tests.Providers
{
    using System.Threading.Tasks;
    using Husa.Extensions.Authorization.Enums;
    using Husa.Extensions.Authorization.Filters;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class TestableRolesFilterAttribute : RolesFilterAttribute
    {
        public TestableRolesFilterAttribute(UserRole[] userRoles = null, RoleEmployee[] employeeRoles = null)
            : base(userRoles, employeeRoles)
        {
        }

        public new Task<bool> AuthorizeAsync(ActionExecutingContext context)
        {
            return base.AuthorizeAsync(context);
        }
    }
}
