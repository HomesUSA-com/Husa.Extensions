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
            var errorMessage = string.Empty;
            var handledApiError = new ApiError(errorMessage);
            switch (context.Exception)
            {
                case NotFoundException notFoundException:
                    handledApiError.Message = notFoundException.Message;
                    context.Result = new NotFoundObjectResult(handledApiError);
                    break;
                case HttpRequestException httpException when httpException.StatusCode == HttpStatusCode.NotFound:
                    handledApiError.Message = httpException.Message;
                    context.Result = new NotFoundObjectResult(handledApiError);
                    break;
                case HttpRequestException httpException:
                    handledApiError.Message = httpException.Message;
                    context.Result = new BadRequestObjectResult(handledApiError);
                    break;
                case DomainException domainException:
                    handledApiError.Message = domainException.Message;
                    context.Result = new BadRequestObjectResult(handledApiError);
                    break;
                default:
#if !DEBUG
                    var apiError = new ApiError("An unhandled error occurred.")
                    {
                        Detail = null,
                    };
#else
                    var exceptionMessage = context.Exception.GetBaseException().Message;
                    string stackTrace = context.Exception.StackTrace;

                    var apiError = new ApiError(exceptionMessage)
                    {
                        Detail = stackTrace,
                    };
#endif
                    errorMessage = "The request could not be served.";
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Result = new JsonResult(apiError);
                    break;
            }

            this.logger.LogError(context.Exception, "errorMessage: {errorMessage}", errorMessage);
            context.ExceptionHandled = true;
            return base.OnExceptionAsync(context);
        }
    }
}
