namespace Husa.Extensions.Downloader.Trestle.Models
{
    using System;

    public class MarketOptions
    {
        public const string Section = "MarketConfiguration";
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string LoginUrl { get; set; }
        public TimeSpan Timeout { get; set; }
        public UriKind UriType { get; set; } = UriKind.Absolute;
        public string BaseUrl { get; set; }
        public int MarketLimit { get; set; }

        public MarketOptions()
        {
            this.Timeout = TimeSpan.FromHours(1);
        }
    }
}
