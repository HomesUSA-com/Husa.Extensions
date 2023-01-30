namespace Husa.Extensions.Downloader.Trestle.Models
{
    using System.Collections.Generic;

    public class GroupMedia
    {
        public string ListingKey { get; set; }
        public IEnumerable<Media> Media { get; set; }
    }
}
