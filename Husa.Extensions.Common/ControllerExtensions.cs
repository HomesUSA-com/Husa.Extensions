namespace Husa.Extensions.Common
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Husa.Extensions.Common.Classes;
    using Husa.Extensions.Common.Enums;
    using Microsoft.AspNetCore.Mvc;

    public static class ControllerExtensions
    {
        public static IActionResult ToActionResult<T>(this ControllerBase controllerBase, CommandResult<T> commandResult)
            where T : class
        {
            if (commandResult.Code == ResponseCode.Error)
            {
                return commandResult.HasErrors()
                    ? controllerBase.BadRequest(new { commandResult.Message, Errors = commandResult.Results })
                    : controllerBase.BadRequest(new { commandResult.Message });
            }

            if (commandResult.HasResults())
            {
                return controllerBase.Ok(commandResult.Results);
            }

            return controllerBase.OkMessage(commandResult.Message);
        }

        public static IActionResult ToActionResult<TResult, TError>(this ControllerBase controllerBase, CommandSingleResult<TResult, TError> commandResult)
        {
            if (commandResult.Code == ResponseCode.Error)
            {
                if (!commandResult.HasErrors())
                {
                    return controllerBase.BadRequest(new { commandResult.Message });
                }

                if (commandResult.Errors is IEnumerable<ValidationResult> errors)
                {
                    return controllerBase.BadRequest(new { commandResult.Message, Errors = errors.ExtractErrors() });
                }

                return controllerBase.BadRequest(new { commandResult.Message, commandResult.Errors });
            }

            if (commandResult.HasResult())
            {
                return controllerBase.Ok(commandResult.Result);
            }

            return controllerBase.OkMessage(commandResult.Message);
        }

        private static List<ValidationResult> ExtractErrors(this IEnumerable<ValidationResult> validationResults)
        {
            var errors = new List<ValidationResult>();

            foreach (var validationResult in validationResults)
            {
                errors.Add(validationResult);
                if (validationResult is CompositeValidationResult compositeResult)
                {
                    errors.AddRange(ExtractErrors(compositeResult.Results));
                }
            }

            return errors;
        }

        private static IActionResult OkMessage(this ControllerBase controllerBase, string message)
        {
            var objectMessage = new { Message = message };
            return controllerBase.Ok(objectMessage);
        }
    }
}
