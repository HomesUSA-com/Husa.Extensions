namespace Husa.Extensions.Downloader.Trestle.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Husa.Extensions.Downloader.Trestle.Models;

    public interface ITrestleClient
    {
        Task<IEnumerable<Member>> GetAgents(DateTimeOffset? modificationTimestamp = null, string filter = null);
        Task<IEnumerable<Office>> GetOffices(DateTimeOffset? modificationTimestamp = null, string filter = null);
        Task<IEnumerable<Property>> GetListings(DateTimeOffset? modificationTimestamp = null, string filter = null, bool expand = false);
        Task<IEnumerable<GroupEntity<Media>>> GetMedia(IEnumerable<string> listingKey);
        Task<IEnumerable<GroupEntity<PropertyRooms>>> GetRooms(IEnumerable<string> listingKey);
        Task<IEnumerable<GroupEntity<OpenHouse>>> GetOpenHouse(IEnumerable<string> listingKey);
    }
}
