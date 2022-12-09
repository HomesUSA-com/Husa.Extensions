namespace Husa.Extensions.Api.Middleware
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public class ProductVersionHeaderMiddleware
    {
        private readonly RequestDelegate next;
        public ProductVersionHeaderMiddleware(RequestDelegate next)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public Task InvokeAsync(HttpContext context)
        {
            if (context?.Response?.Headers is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Response.Headers.Add("X-APP-VERSION", Assembly.GetEntryAssembly()?.GetName()?.Version?.ToString());

            return this.next(context);
        }
    }
}
