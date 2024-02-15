namespace Husa.Extensions.Common
{
    using System.Linq;
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
                if (commandResult.HasErrors())
                {
                    return controllerBase.BadRequest(commandResult.Results);
                }

                return controllerBase.BadRequest(commandResult.Message);
            }

            if (commandResult.HasResults())
            {
                return commandResult.Results.Count() == 1 ? controllerBase.Ok(commandResult.Results.Single()) : controllerBase.Ok(commandResult.Results);
            }

            return controllerBase.Ok(commandResult.Message);
        }

        public static IActionResult ToActionResult<TResult, TError>(this ControllerBase controllerBase, CommandSingleResult<TResult, TError> commandResult)
        {
            if (commandResult.Code == ResponseCode.Error)
            {
                if (commandResult.HasErrors())
                {
                    return controllerBase.BadRequest(commandResult.Errors);
                }

                return controllerBase.BadRequest(commandResult.Message);
            }

            if (commandResult.HasResult())
            {
                return controllerBase.Ok(commandResult.Result);
            }

            return controllerBase.Ok(commandResult.Message);
        }
    }
}
