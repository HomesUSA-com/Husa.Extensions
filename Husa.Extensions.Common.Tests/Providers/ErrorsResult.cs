namespace Husa.Extensions.Common.Tests.Providers
{
    using System.Collections.Generic;

    public class ErrorsResult<T>
    {
        public string Message { get; set; }
        public IEnumerable<T> Errors { get; set; }
    }
}
