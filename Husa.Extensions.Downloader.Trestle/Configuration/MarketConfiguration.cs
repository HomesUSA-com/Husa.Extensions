namespace Husa.Extensions.Downloader.Trestle.Configuration
{
    using System.Collections.Generic;

    public class MarketConfiguration
    {
        public bool WhiteListEnabled { get; set; }
        public bool ProcessFullListing { get; set; }
        public IEnumerable<string> WhiteListBuilders { get; set; }
    }
}
