namespace Husa.Extensions.Downloader.RetsSdk.Models
{
    using Husa.Extensions.Downloader.RetsSdk.Models.Enums;
    using System;

    public class ConnectionOptions
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public AuthenticationType Type { get; set; }
        public string UserAgent { get; set; }
        public string UserAgentPassward { get; set; }
        public SupportedRetsVersion RetsServerVersion { get; set; } = SupportedRetsVersion.Version_1_7_2;
        public string LoginUrl { get; set; }
        public TimeSpan Timeout { get; set; }
        public UriKind UriType { get; set; } = UriKind.Absolute;
        public string BaseUrl { get; set; }

        public ConnectionOptions()
        {
            Timeout = TimeSpan.FromHours(1);
        }

        public RetsVersion Version => new RetsVersion(RetsServerVersion);
    }
}
