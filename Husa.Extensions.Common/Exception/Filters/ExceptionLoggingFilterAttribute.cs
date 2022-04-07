namespace Husa.Extensions.Common.Exception.Filters
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;

    public class ExceptionLoggingFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionLoggingFilterAttribute> logger;

        public ExceptionLoggingFilterAttribute(ILogger<ExceptionLoggingFilterAttribute> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var exceptionMessage = context.Exception.GetBaseException().Message;
            var errorMessage = string.Empty;
            switch (context.Exception)
            {
                case NotFoundException notFoundException:
                    errorMessage = notFoundException.Message;
                    context.Result = new NotFoundObjectResult(notFoundException.Message);
                    break;
                case HttpRequestException httpException when httpException.StatusCode == HttpStatusCode.NotFound:
                    errorMessage = httpException.Message;
                    context.Result = new NotFoundObjectResult(httpException.Message);
                    break;
                case HttpRequestException httpException:
                    errorMessage = httpException.Message;
                    context.Result = new BadRequestObjectResult(httpException.Message);
                    break;
                default:
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Result = new BadRequestObjectResult(exceptionMessage);
                    break;
            }

            this.logger.LogError(context.Exception, errorMessage);
            context.ExceptionHandled = true;
            return base.OnExceptionAsync(context);
        }
    }
}
