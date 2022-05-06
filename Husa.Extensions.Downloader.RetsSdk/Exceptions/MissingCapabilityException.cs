namespace Husa.Extensions.Downloader.RetsSdk.Exceptions
{
    using System;
    public class MissingCapabilityException : Exception
    {
        public MissingCapabilityException()
            : base("The requested capability does not exists")
        {
        }
    }
}
