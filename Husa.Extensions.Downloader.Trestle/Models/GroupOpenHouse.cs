namespace Husa.Extensions.Downloader.Trestle.Models
{
    using System.Collections.Generic;

    public class GroupOpenHouse
    {
        public string ListingKey { get; set; }
        public IEnumerable<OpenHouse> OpenHouse { get; set; }
    }
}
