namespace Husa.Extensions.Downloader.Trestle.Models
{
    using System.Collections.Generic;

    public class GroupEntity<T>
    {
        public string Id { get; set; }
        public IEnumerable<T> Values { get; set; }
    }
}
