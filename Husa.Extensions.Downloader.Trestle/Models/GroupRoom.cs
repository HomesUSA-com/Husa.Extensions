namespace Husa.Extensions.Downloader.Trestle.Models
{
    using System.Collections.Generic;

    public class GroupRoom
    {
        public string ListingKey { get; set; }
        public IEnumerable<PropertyRooms> Rooms { get; set; }
    }
}
