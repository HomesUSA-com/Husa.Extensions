namespace Husa.Extensions.Downloader.Trestle.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Husa.Extensions.Downloader.Trestle.Configuration;
    using Husa.Extensions.Downloader.Trestle.Helpers;
    using Husa.Extensions.Downloader.Trestle.Helpers.Parsers;
    using Husa.Extensions.Downloader.Trestle.Models;
    using Microsoft.Extensions.Options;

    public class TrestleClient : ITrestleClient
    {
        private readonly ITrestleRequester trestleRequester;
        private readonly IBlobTableRepository tableStorageRepository;
        private readonly MarketOptions connectionOptions;
        private readonly BlobOptions blobOptions;
        private AuthInfo authInfo = null;

        public TrestleClient(ITrestleRequester trestleRequester, IBlobTableRepository tableStorageRepository, IOptions<MarketOptions> connectionOptions, IOptions<BlobOptions> blobOptions)
        {
            this.tableStorageRepository = tableStorageRepository ?? throw new ArgumentNullException(nameof(tableStorageRepository));
            this.trestleRequester = trestleRequester ?? throw new ArgumentNullException(nameof(trestleRequester));
            this.connectionOptions = connectionOptions.Value ?? throw new ArgumentNullException(nameof(connectionOptions));
            this.blobOptions = blobOptions.Value ?? throw new ArgumentNullException(nameof(blobOptions));
        }

        public async Task<IEnumerable<Member>> GetAgents(DateTimeOffset? modificationTimestamp = null, string filter = null)
        {
            var queryFilter = Utils.GetFilter<Member>(modificationTimestamp, filter);
            var client = await this.GetAuthenticatedClient();
            var agents = await this.trestleRequester.GetData<Member>(client, resource: "Member", queryFilter);

            return agents;
        }

        public async Task<IEnumerable<Office>> GetOffices(DateTimeOffset? modificationTimestamp = null, string filter = null)
        {
            var queryFilter = Utils.GetFilter<Office>(modificationTimestamp, filter);
            var client = await this.GetAuthenticatedClient();
            var offices = await this.trestleRequester.GetData<Office>(client, resource: "Office", queryFilter);

            return offices;
        }

        public async Task<IEnumerable<Property>> GetListings(DateTimeOffset? modificationTimestamp = null, string filter = null, bool expand = false)
        {
            var queryFilter = Utils.GetFilter<Property>(modificationTimestamp, filter, expand);
            var client = await this.GetAuthenticatedClient();
            var listings = await this.trestleRequester.GetData<Property>(client, resource: "Property", queryFilter);

            return listings;
        }

        public async Task<IEnumerable<GroupEntity<Media>>> GetMedia(IEnumerable<string> listingsKeys)
        {
            var listingKeyString = string.Join(",", listingsKeys.Select(listingKey => { return $"'{listingKey}'"; }));
            var queryFilter = Utils.GetFilter<Media>(filter: listingKeyString);
            var client = await this.GetAuthenticatedClient();
            var media = await this.trestleRequester.GetData<Media>(client, resource: "Media", queryFilter);
            var groupMedia = media.GroupBy(x => x.ResourceRecordKey).Select(group => new GroupEntity<Media>
            {
                Id = group.Key,
                Values = group.ToList(),
            });

            return groupMedia;
        }

        public async Task<IEnumerable<MultipartImage>> GetMediaStream(string listingId)
        {
            var client = await this.GetAuthenticatedClient();
            var stream = await this.trestleRequester.GetMediaStream(client, listingId);
            var multipartParser = new MultipartParser(stream);
            return multipartParser.GetImages();
        }

        public async Task<IEnumerable<GroupEntity<PropertyRooms>>> GetRooms(IEnumerable<string> listingsKeys)
        {
            var listingKeyString = string.Join(",", listingsKeys.Select(listingKey => { return $"'{listingKey}'"; }));
            var queryFilter = Utils.GetFilter<PropertyRooms>(filter: listingKeyString);
            var client = await this.GetAuthenticatedClient();
            var rooms = await this.trestleRequester.GetData<PropertyRooms>(client, resource: "PropertyRooms", queryFilter);

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
            var openHouse = await this.trestleRequester.GetData<OpenHouse>(client, resource: "OpenHouse", queryFilter);

            var groupOpenHouse = openHouse.GroupBy(x => x.ListingKey).Select(group => new GroupEntity<OpenHouse>
            {
                Id = group.Key,
                Values = group.ToList(),
            });

            return groupOpenHouse;
        }

        public async Task<IEnumerable<Teams>> GetTeams(DateTimeOffset? modificationTimestamp = null, string filter = null)
        {
            var queryFilter = Utils.GetFilter<Teams>(modificationTimestamp, filter);
            var client = await this.GetAuthenticatedClient();
            var teams = await this.trestleRequester.GetData<Teams>(client, resource: "Teams", queryFilter);

            return teams;
        }

        public async Task<IEnumerable<GroupEntity<TeamMembers>>> GetTeamMembers(IEnumerable<string> teamKeys)
        {
            var teamKeyString = string.Join(",", teamKeys.Select(teamKey => { return $"'{teamKey}'"; }));
            var queryFilter = Utils.GetFilter<TeamMembers>(filter: teamKeyString);
            var client = await this.GetAuthenticatedClient();
            var teamMembers = await this.trestleRequester.GetData<TeamMembers>(client, resource: "TeamMembers", queryFilter);

            var groupTeamMembers = teamMembers.GroupBy(x => x.TeamKey).Select(group => new GroupEntity<TeamMembers>
            {
                Id = group.Key,
                Values = group.ToList(),
            });

            return groupTeamMembers;
        }

        public void Login(string clientId, string clientSecret, string partitionKey)
        {
            this.authInfo = new AuthInfo(clientId, clientSecret, partitionKey);
        }

        private async Task<HttpClient> GetAuthenticatedClient()
        {
            if (this.authInfo == null)
            {
                this.InitializeAuthInfo();
            }

            var tokenInfo = await this.tableStorageRepository.GetTokenInfoFromStorage(this.authInfo);
            if (!Utils.IsValidToken(tokenInfo))
            {
                var newTokenInfo = await this.trestleRequester.GetTokenInfo(this.authInfo);
                await this.tableStorageRepository.SaveTokenInfo(newTokenInfo, this.authInfo);
                return this.trestleRequester.GetAuthenticatedClient(newTokenInfo.AccessToken);
            }

            return this.trestleRequester.GetAuthenticatedClient(tokenInfo.AccessToken);
        }

        private void InitializeAuthInfo()
        {
            this.authInfo = new AuthInfo(this.connectionOptions.ClientId, this.connectionOptions.ClientSecret, this.blobOptions.PartitionKey);
        }
    }
}
