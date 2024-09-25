namespace Husa.Extensions.Downloader.Trestle.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Husa.Extensions.Common.Enums;
    using Husa.Extensions.Downloader.Trestle.Models.Enums;

    public class MarketOptions
    {
        public const string Section = "MarketConfiguration";

        public MarketOptions()
        {
            this.Timeout = TimeSpan.FromHours(1);
        }

        [Required(AllowEmptyStrings = false)]
        public string ClientId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string ClientSecret { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string LoginUrl { get; set; }
        public TimeSpan Timeout { get; set; }
        public UriKind UriType { get; set; } = UriKind.Absolute;
        public string BaseUrl { get; set; }
        public IEnumerable<SystemOrigin> Market { get; set; }
        public IEnumerable<PropertyType> GlobalPropertyTypes { get; set; }
        public int MarketLimit { get; set; }
        [Required]
        public IDictionary<MarketCode, MarketConfiguration> MarketConfiguration { get; set; }
    }
}
