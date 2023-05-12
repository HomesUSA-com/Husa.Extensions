namespace Husa.Extensions.Common.Classes
{
    using System.Collections.Generic;
    using System.Linq;
    using Husa.Extensions.Common.Enums;

    public sealed class CommandSingleResult<TResult, TError>
        where TResult : class
        where TError : class
    {
        public CommandSingleResult()
        {
            this.Code = ResponseCode.Success;
            this.Errors = new List<TError>();
        }

        private CommandSingleResult(ResponseCode code)
            : this()
        {
            this.Code = code;
        }

        private CommandSingleResult(ResponseCode code, string message)
            : this(code)
        {
            this.Message = message;
        }

        private CommandSingleResult(ResponseCode code, TResult result, string message = null)
            : this(code, message)
        {
            this.Result = result;
        }

        private CommandSingleResult(ResponseCode code, IEnumerable<TError> errors, string message = null)
            : this(code, message)
        {
            this.Errors = errors;
        }

        public ResponseCode Code { get; set; }

        public string Message { get; set; } = string.Empty;

        public TResult Result { get; set; }

        public IEnumerable<TError> Errors { get; set; }

        public static CommandSingleResult<TResult, TError> Success() => new(ResponseCode.Success);

        public static CommandSingleResult<TResult, TError> Success(TResult data) => new(ResponseCode.Success, data);

        public static CommandSingleResult<TResult, TError> Information(string message) => new(ResponseCode.Information, message);

        public static CommandSingleResult<TResult, TError> Information(TResult data, string message) => new(ResponseCode.Information, data, message);

        public static CommandSingleResult<TResult, TError> Error(string message) => new(ResponseCode.Error, message);

        public static CommandSingleResult<TResult, TError> Error(TError error, string message = null) => new(ResponseCode.Error, new[] { error }, message);

        public static CommandSingleResult<TResult, TError> Error(IEnumerable<TError> errors, string message = null) => new(ResponseCode.Error, errors, message);

        public bool HasErrors() => this.Code == ResponseCode.Error && this.Errors != null && this.Errors.Any();

        public bool HasResult() => this.Code != ResponseCode.Error && this.Result != null;
    }
}
