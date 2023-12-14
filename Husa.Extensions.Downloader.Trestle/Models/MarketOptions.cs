namespace Husa.Extensions.Downloader.Trestle.Models
{
    using System;
    using System.Collections.Generic;
    using Husa.Extensions.Downloader.Trestle.Models.Enums;

    public class MarketOptions
    {
        public const string Section = "MarketConfiguration";

        public MarketOptions()
        {
            this.Timeout = TimeSpan.FromHours(1);
        }

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string LoginUrl { get; set; }
        public TimeSpan Timeout { get; set; }
        public UriKind UriType { get; set; } = UriKind.Absolute;
        public string BaseUrl { get; set; }
        public IEnumerable<SystemOrigin> Market { get; set; }
        public int MarketLimit { get; set; }
    }
}
