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
            var response = await this.trestleRequester.GetData<Member>(client, "Member", queryFilter);

            return response;
        }

        public async Task<IEnumerable<Office>> GetOffices(DateTimeOffset? modificationTimestamp = null, string filter = null)
        {
            var queryFilter = Utils.GetFilter<Office>(modificationTimestamp, filter);
            var client = await this.GetAuthenticatedClient();
            var response = await this.trestleRequester.GetData<Office>(client, "Office", queryFilter);

            return response;
        }

        public async Task<IEnumerable<Property>> GetListings(DateTimeOffset? modificationTimestamp = null, string filter = null, bool expand = false)
        {
            var queryFilter = Utils.GetFilter<Property>(modificationTimestamp, filter, expand);
            var client = await this.GetAuthenticatedClient();
            var response = await this.trestleRequester.GetData<Property>(client, "Property", queryFilter);

            return response;
        }

        public async Task<IEnumerable<GroupMedia>> GetMedia(IEnumerable<string> listingKey)
        {
            var listingKeyString = string.Join(",", listingKey.Select(c => { return $"'{c}'"; }));
            var queryFilter = Utils.GetFilter<Media>(filter: listingKeyString);
            var client = await this.GetAuthenticatedClient();
            var response = await this.trestleRequester.GetData<Media>(client, "Media", queryFilter);
            var groupMedia = response.GroupBy(x => x.ResourceRecordKey).Select(group => new GroupMedia
            {
                ListingKey = group.Key,
                Media = group.ToList(),
            });

            return groupMedia;
        }

        public async Task<IEnumerable<GroupRoom>> GetRooms(IEnumerable<string> listingKey)
        {
            var listingKeyString = string.Join(",", listingKey.Select(c => { return $"'{c}'"; }));
            var queryFilter = Utils.GetFilter<PropertyRooms>(filter: listingKeyString);
            var client = await this.GetAuthenticatedClient();
            var response = await this.trestleRequester.GetData<PropertyRooms>(client, "PropertyRooms", queryFilter);

            var groupRoom = response.GroupBy(x => x.ListingKey).Select(group => new GroupRoom
            {
                ListingKey = group.Key,
                Rooms = group.ToList(),
            });

            return groupRoom;
        }

        public async Task<IEnumerable<GroupOpenHouse>> GetOpenHouse(IEnumerable<string> listingKey)
        {
            var listingKeyString = string.Join(",", listingKey.Select(c => { return $"'{c}'"; }));
            var queryFilter = Utils.GetFilter<OpenHouse>(filter: listingKeyString);
            var client = await this.GetAuthenticatedClient();
            var response = await this.trestleRequester.GetData<OpenHouse>(client, "OpenHouse", queryFilter);

            var groupOpenHouse = response.GroupBy(x => x.ListingKey).Select(group => new GroupOpenHouse
            {
                ListingKey = group.Key,
                OpenHouse = group.ToList(),
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
