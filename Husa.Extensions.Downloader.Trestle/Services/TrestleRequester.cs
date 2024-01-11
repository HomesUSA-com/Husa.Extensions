namespace Husa.Extensions.Downloader.Trestle.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Threading.Tasks;
    using System.Xml;
    using Husa.Extensions.Downloader.Trestle.Contracts;
    using Husa.Extensions.Downloader.Trestle.Helpers;
    using Husa.Extensions.Downloader.Trestle.Helpers.Converters;
    using Husa.Extensions.Downloader.Trestle.Models;
    using Microsoft.Extensions.Options;

    public class TrestleRequester : ITrestleRequester
    {
        private readonly MarketOptions connectionOptions;
        private readonly JsonSerializerOptions jsonSerializerOptions;
        private readonly IHttpClientFactory httpClientFactory;

        public TrestleRequester(IOptions<MarketOptions> connectionOptions, IHttpClientFactory httpClientFactory)
        {
            this.connectionOptions = connectionOptions.Value ?? throw new ArgumentNullException(nameof(connectionOptions));
            this.httpClientFactory = httpClientFactory;
            this.jsonSerializerOptions = new JsonSerializerOptions { Converters = { new BoolConverter(), new DateTimeConverter() } };
        }

        public HttpClient GetAuthenticatedClient(string accessToken)
        {
            var client = this.httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(this.connectionOptions.BaseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return client;
        }

        public async Task<AuthenticationResult> GetTokenInfo(AuthInfo authInfo)
        {
            var client = this.httpClientFactory.CreateClient();

            var stringContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("client_id", authInfo.ClientId),
                    new KeyValuePair<string, string>("client_secret", authInfo.ClientSecret),
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("scope", "api"),
                });

            var httpResponse = await client.PostAsync(this.connectionOptions.LoginUrl, stringContent);
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadFromJsonAsync<AuthenticationResult>(this.jsonSerializerOptions);

            return result;
        }

        public async Task<IEnumerable<T>> GetData<T>(HttpClient client, string resource, string filter = null)
        {
            var uri = $"odata/{resource}";
            if (resource == "Property")
            {
                filter = Utils.AddSystemOriginFilter(filter, this.connectionOptions.Market);
            }

            if (!string.IsNullOrEmpty(filter))
            {
                uri += filter;
            }

            var result = await this.GetData<T>(client, uri);
            return result;
        }

        public async Task<Stream> GetMediaStream(HttpClient client, string entityKey)
        {
            var uri = $"odata/Property('{entityKey}')/Media/All";
            return await client.GetStreamAsync(uri);
        }

        public async Task<XmlDocument> GetMetadata(HttpClient client)
        {
            var response = await client.GetAsync("odata/$metadata");
            var body = await response.Content.ReadAsStringAsync();
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(body);
            return xmlDocument;
        }

        private async Task<IEnumerable<T>> GetData<T>(HttpClient client, string nextLink)
        {
            var httpResponse = await client.GetAsync(nextLink);
            httpResponse.EnsureSuccessStatusCode();
            var response = await httpResponse.Content.ReadFromJsonAsync<ODataResponse<T>>(this.jsonSerializerOptions);
            var result = response.Value;

            if (response.NextLink != null)
            {
                result = result.Concat(await this.GetData<T>(client, response.NextLink));
            }

            return result;
        }
    }
}
