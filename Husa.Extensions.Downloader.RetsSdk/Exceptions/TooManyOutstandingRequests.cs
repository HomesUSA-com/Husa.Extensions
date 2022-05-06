namespace Husa.Extensions.Downloader.RetsSdk.Exceptions
{
    using System;

    public class TooManyOutstandingRequests : Exception
    {
        public TooManyOutstandingRequests()
            : base("Too many outstanding requests")
        {
        }

        public TooManyOutstandingRequests(string message)
            : base(message)
        {
        }
    }
}
