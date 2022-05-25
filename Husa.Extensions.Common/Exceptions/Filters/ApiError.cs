namespace Husa.Extensions.Common.Exceptions.Filters
{
    public class ApiError
    {
        public ApiError(string message)
        {
            this.Message = message;
            this.IsError = true;
        }

        public string Message { get; set; }

        public bool IsError { get; set; }

        public string Detail { get; set; }
    }
}
