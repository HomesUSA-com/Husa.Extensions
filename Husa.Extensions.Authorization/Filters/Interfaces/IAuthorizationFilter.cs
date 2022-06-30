namespace Husa.Extensions.Authorization.Filters.Interfaces
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Filters;

    internal interface IAuthorizationFilter : IAsyncAuthorizationFilter
    {
        Task GetCompanyEmployeeAsync(AuthorizationFilterContext context, IUserContext user);
    }
}
