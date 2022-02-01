namespace Husa.Extensions.Authorization.Filters
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class RoleAuthorizeAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return this.AuthorizeContext(context, next);
        }

        public virtual async Task<bool> IsAuthorizedAsync(ActionExecutingContext context)
        {
            if (this.HasAttribute<AllowAnonymousAttribute>(context))
            {
                return true;
            }

            return await this.AuthorizeAsync(context);
        }

        protected virtual Task<bool> AuthorizeAsync(ActionExecutingContext context)
        {
            ////check
            return Task.FromResult(true);
        }

        protected bool HasAttribute<T>(ActionExecutingContext context)
            where T : Attribute
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var controllerDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            return controllerDescriptor.MethodInfo.GetCustomAttributes(typeof(T), true).Any();
        }

        private async Task AuthorizeContext(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!await this.IsAuthorizedAsync(context))
            {
                context.Result = new ForbidResult();
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
