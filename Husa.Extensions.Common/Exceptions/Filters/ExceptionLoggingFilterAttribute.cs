namespace Husa.Extensions.Common.Exceptions.Filters
{
    using System;
    using System.Net.Http;
    using Microsoft.AspNetCore.Http;
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

        public override void OnException(ExceptionContext context)
        {
            var errorMessage = context.Exception.GetBaseException()?.Message
                ?? context.Exception.Message
                ?? "An unhandled error occurred.";
            this.logger.LogError(context.Exception, "errorMessage: {errorMessage}", errorMessage);

            ApiError apiError = new(errorMessage);
#if DEBUG
            apiError.Detail = context.Exception.StackTrace;
#endif

            var result = new ObjectResult(apiError)
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };

            // Handle specific exceptions with different status codes
            switch (context.Exception)
            {
                case EntityAlreadyExistsException:
                    result.StatusCode = StatusCodes.Status409Conflict;
                    break;
                case UnauthorizedAccessException:
                    result.StatusCode = StatusCodes.Status403Forbidden;
                    break;
                case NotFoundException:
                    result.StatusCode = StatusCodes.Status404NotFound;
                    break;
                case ArgumentNullException:
                case ArgumentException:
                case DomainException:
                case InvalidOperationException:
                    result.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case HttpRequestException httpException:
                    result.StatusCode = (int)httpException.StatusCode;
                    break;
            }

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
