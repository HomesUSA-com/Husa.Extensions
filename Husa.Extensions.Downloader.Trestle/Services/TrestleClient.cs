namespace Husa.Extensions.Downloader.Trestle.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Husa.Extensions.Downloader.Trestle.Helpers;
    using Husa.Extensions.Downloader.Trestle.Models;

    public class TrestleClient : ITrestleClient
    {
        private readonly ITrestleRequester trestleRequester;
        private readonly IBlobTableRepository tableStorageRepository;

        public TrestleClient(ITrestleRequester trestleRequester, IBlobTableRepository tableStorageRepository)
        {
            this.tableStorageRepository = tableStorageRepository ?? throw new ArgumentNullException(nameof(tableStorageRepository));
            this.trestleRequester = trestleRequester ?? throw new ArgumentNullException(nameof(trestleRequester));
        }

        public async Task<IEnumerable<Member>> GetAgents(DateTimeOffset? modificationTimestamp = null, string filter = null)
        {
            var queryFilter = Utils.GetFilter<Member>(modificationTimestamp, filter);
            var client = await this.GetAuthenticatedClient();
            var agents = await this.trestleRequester.GetData<Member>(client, "Member", queryFilter);

            return agents;
        }

        public async Task<IEnumerable<Office>> GetOffices(DateTimeOffset? modificationTimestamp = null, string filter = null)
        {
            var queryFilter = Utils.GetFilter<Office>(modificationTimestamp, filter);
            var client = await this.GetAuthenticatedClient();
            var offices = await this.trestleRequester.GetData<Office>(client, "Office", queryFilter);

            return offices;
        }

        public async Task<IEnumerable<Property>> GetListings(DateTimeOffset? modificationTimestamp = null, string filter = null, bool expand = false)
        {
            var queryFilter = Utils.GetFilter<Property>(modificationTimestamp, filter, expand);
            var client = await this.GetAuthenticatedClient();
            var listings = await this.trestleRequester.GetData<Property>(client, "Property", queryFilter);

            return listings;
        }

        public async Task<IEnumerable<GroupEntity<Media>>> GetMedia(IEnumerable<string> listingsKeys)
        {
            var listingKeyString = string.Join(",", listingsKeys.Select(listingKey => { return $"'{listingKey}'"; }));
            var queryFilter = Utils.GetFilter<Media>(filter: listingKeyString);
            var client = await this.GetAuthenticatedClient();
            var media = await this.trestleRequester.GetData<Media>(client, "Media", queryFilter);
            var groupMedia = media.GroupBy(x => x.ResourceRecordKey).Select(group => new GroupEntity<Media>
            {
                Id = group.Key,
                Values = group.ToList(),
            });

            return groupMedia;
        }

        public async Task<IEnumerable<GroupEntity<PropertyRooms>>> GetRooms(IEnumerable<string> listingsKeys)
        {
            var listingKeyString = string.Join(",", listingsKeys.Select(listingKey => { return $"'{listingKey}'"; }));
            var queryFilter = Utils.GetFilter<PropertyRooms>(filter: listingKeyString);
            var client = await this.GetAuthenticatedClient();
            var rooms = await this.trestleRequester.GetData<PropertyRooms>(client, "PropertyRooms", queryFilter);

            var groupRoom = rooms.GroupBy(x => x.ListingKey).Select(group => new GroupEntity<PropertyRooms>
            {
                Id = group.Key,
                Values = group.ToList(),
            });

            return groupRoom;
        }

        public async Task<IEnumerable<GroupEntity<OpenHouse>>> GetOpenHouse(IEnumerable<string> listingsKeys)
        {
            var listingKeyString = string.Join(",", listingsKeys.Select(listingKey => { return $"'{listingKey}'"; }));
            var queryFilter = Utils.GetFilter<OpenHouse>(filter: listingKeyString);
            var client = await this.GetAuthenticatedClient();
            var openHouse = await this.trestleRequester.GetData<OpenHouse>(client, "OpenHouse", queryFilter);

            var groupOpenHouse = openHouse.GroupBy(x => x.ListingKey).Select(group => new GroupEntity<OpenHouse>
            {
                Id = group.Key,
                Values = group.ToList(),
            });

            return groupOpenHouse;
        }

        private async Task<HttpClient> GetAuthenticatedClient()
        {
            var tokenInfo = await this.tableStorageRepository.GetTokenInfoFromStorage();
            if (!Utils.IsValidToken(tokenInfo))
            {
                var newTokenInfo = await this.trestleRequester.GetTokenInfo();
                await this.tableStorageRepository.SaveTokenInfo(newTokenInfo);
                return this.trestleRequester.GetAuthenticatedClient(newTokenInfo.AccessToken);
            }

            return this.trestleRequester.GetAuthenticatedClient(tokenInfo.AccessToken);
        }
    }
}
