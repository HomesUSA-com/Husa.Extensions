namespace Husa.Extensions.Downloader.Trestle.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Husa.Extensions.Downloader.Trestle.Helpers.Parsers;
    using Husa.Extensions.Downloader.Trestle.Models;

    public interface ITrestleClient
    {
        void Login(string clientId, string clientSecret, string partitionKey);
        Task<IEnumerable<Member>> GetAgents(DateTimeOffset? modificationTimestamp = null, string filter = null);
        Task<IEnumerable<Office>> GetOffices(DateTimeOffset? modificationTimestamp = null, string filter = null);
        Task<IEnumerable<Property>> GetListings(DateTimeOffset? modificationTimestamp = null, string filter = null, bool expand = false);
        Task<IEnumerable<GroupEntity<Media>>> GetMedia(IEnumerable<string> listingsKeys);
        Task<IEnumerable<GroupEntity<PropertyRooms>>> GetRooms(IEnumerable<string> listingsKeys);
        Task<IEnumerable<GroupEntity<OpenHouse>>> GetOpenHouse(IEnumerable<string> listingsKeys);
        Task<IEnumerable<MultipartImage>> GetMediaStream(string listingId);
    }
}
