namespace Husa.Extensions.Common
{
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
                return commandResult.HasErrors()
                    ? controllerBase.BadRequest(new { commandResult.Message, commandResult.Errors })
                    : controllerBase.BadRequest(new { commandResult.Message });
            }

            if (commandResult.HasResult())
            {
                return controllerBase.Ok(commandResult.Result);
            }

            return controllerBase.OkMessage(commandResult.Message);
        }

        private static IActionResult OkMessage(this ControllerBase controllerBase, string message)
        {
            var objectMessage = new { Message = message };
            return controllerBase.Ok(objectMessage);
        }
    }
}
