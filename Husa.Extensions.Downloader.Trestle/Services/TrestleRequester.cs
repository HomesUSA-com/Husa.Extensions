namespace Husa.Extensions.Downloader.Trestle.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Xml;
    using Husa.Extensions.Downloader.Trestle.Contracts;
    using Husa.Extensions.Downloader.Trestle.Models;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class TrestleRequester : ITrestleRequester
    {
        private const string PAGESIZE = "1000";
        private readonly MarketOptions connectionOptions;
        private readonly JsonSerializerSettings jsonSerializerSettings;
        private readonly IHttpClientFactory httpClientFactory;

        public TrestleRequester(IOptions<MarketOptions> connectionOptions, IHttpClientFactory httpClientFactory)
        {
            this.connectionOptions = connectionOptions.Value;
            this.httpClientFactory = httpClientFactory;
            this.jsonSerializerSettings = new JsonSerializerSettings 
            { 
                NullValueHandling = NullValueHandling.Ignore,
                Error = HandleDeserializationError,
            };
        }

        public async Task<HttpClient> GetAuthenticatedClient()
        {
            var stringContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", this.connectionOptions.ClientId),
                new KeyValuePair<string, string>("client_secret", this.connectionOptions.ClientSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("scope", "api"),
            });
            
            var client = httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(this.connectionOptions.BaseUrl);
            var httpResponse = await client.PostAsync(this.connectionOptions.LoginUrl, stringContent);
            httpResponse.EnsureSuccessStatusCode();
            var content = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AuthenticationResult>(content, this.jsonSerializerSettings);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);

            return client;
        }

        public async Task<IEnumerable<T>> GetData<T>(HttpClient client, string resource, string filter)
        {
            var uri = $"odata/{resource}?&$top={PAGESIZE}";
            if (filter != null)
            {
                uri += $"&$filter={filter}";
            }

            var result = await this.GetData<T>(client, uri);
            return result;
        }

        private async Task<IEnumerable<T>> GetData<T>(HttpClient client, string nextLink)
        {
            var httpResponse = await client.GetAsync(nextLink);
            httpResponse.EnsureSuccessStatusCode();
            var content = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ODataResponse<T>>(content, this.jsonSerializerSettings);
            var result = response.Value;


            if (response.NextLink != null)
            {
                result = result.Concat(await this.GetData<T>(client, response.NextLink));
            }
            return result;
        }

        public async Task<XmlDocument> GetMetadata(HttpClient client)
        {
            var response = await client.GetAsync("odata/$metadata");
            var body = await response.Content.ReadAsStringAsync();
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(body);
            return xmlDocument;
        }
        public void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            errorArgs.ErrorContext.Handled = true;
        }
    }
}
