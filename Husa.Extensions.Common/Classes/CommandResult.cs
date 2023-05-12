namespace Husa.Extensions.Common.Classes
{
    using System.Collections.Generic;
    using System.Linq;
    using Husa.Extensions.Common.Enums;

    public sealed class CommandResult<T>
        where T : class
    {
        public CommandResult()
        {
            this.Code = ResponseCode.Success;
            this.Results = new List<T>();
        }

        private CommandResult(ResponseCode code)
            : this()
        {
            this.Code = code;
        }

        private CommandResult(ResponseCode code, string message)
            : this(code)
        {
            this.Message = message;
        }

        private CommandResult(ResponseCode code, IEnumerable<T> results)
            : this(code)
        {
            this.Results = results;
        }

        private CommandResult(ResponseCode code, string message, IEnumerable<T> results)
            : this(code, message)
        {
            this.Results = results;
        }

        public ResponseCode Code { get; set; }

        public string Message { get; set; } = string.Empty;

        public IEnumerable<T> Results { get; set; }

        public T Result => this.HasResults() ? this.Results.Single() : null;

        public static CommandResult<T> Success() => new(ResponseCode.Success);

        public static CommandResult<T> Success(T data) => new(ResponseCode.Success, new[] { data });

        public static CommandResult<T> Success(IEnumerable<T> data) => new(ResponseCode.Success, data);

        public static CommandResult<T> Information(string message) => new(ResponseCode.Information, message);

        public static CommandResult<T> Information(string message, T data) => new(ResponseCode.Information, message, new[] { data });

        public static CommandResult<T> Information(string message, IEnumerable<T> data) => new(ResponseCode.Information, message, data);

        public static CommandResult<T> Error(string message) => new(ResponseCode.Error, message);

        public static CommandResult<T> Error(T error) => new(ResponseCode.Error, new[] { error });

        public static CommandResult<T> Error(IEnumerable<T> errors) => new(ResponseCode.Error, errors);

        public static CommandResult<T> Error(string message, T error) => new(ResponseCode.Error, message, new[] { error });

        public static CommandResult<T> Error(string message, IEnumerable<T> errors) => new(ResponseCode.Error, message, errors);

        public bool HasErrors() => this.Code == ResponseCode.Error && this.Results != null && this.Results.Any();

        public bool HasResults() => this.Code != ResponseCode.Error && this.Results != null && this.Results.Any();
    }
}
