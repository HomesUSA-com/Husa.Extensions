namespace Husa.Extensions.Downloader.Trestle.Configuration
{
    using System.Collections.Generic;
    using Husa.Extensions.Downloader.Trestle.Models.Enums;

    public class MarketConfiguration
    {
        public bool WhiteListEnabled { get; set; }
        public bool ProcessFullListing { get; set; }
        public IEnumerable<string> WhiteListBuilders { get; set; }
        public IEnumerable<PropertyType> LocalPropertyTypes { get; set; }
    }
}
