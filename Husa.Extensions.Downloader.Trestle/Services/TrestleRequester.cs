namespace Husa.Extensions.Downloader.Trestle.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using System.Xml;
    using Husa.Extensions.Downloader.Trestle.Helpers.Extensions;
    using Husa.Extensions.Downloader.Trestle.Models;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;

    public class TrestleRequester : ITrestleRequester
    {
        private readonly MarketOptions connectionOptions;
        private readonly JsonSerializerOptions jsonSerializerOptions;
        private readonly IHttpClientFactory httpClientFactory;

        public TrestleRequester(IOptions<MarketOptions> connectionOptions, IHttpClientFactory httpClientFactory)
        {
            this.connectionOptions = connectionOptions.Value;
            this.httpClientFactory = httpClientFactory;
            this.jsonSerializerOptions = new JsonSerializerOptions().SetConfiguration();
            this.jsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        }

        public TrestleRequester(IOptions<MarketOptions> connectionOptions, IOptions<JsonSerializerOptions> jsonSerializerOptions, IHttpClientFactory httpClientFactory)
        {
            this.connectionOptions = connectionOptions.Value ?? throw new ArgumentNullException(nameof(connectionOptions));
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            this.jsonSerializerOptions = jsonSerializerOptions.Value ?? throw new ArgumentNullException(nameof(jsonSerializerOptions));
            this.jsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
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
            var result = await httpResponse.Content.ReadFromJsonAsync<AuthenticationResult>(this.jsonSerializerOptions);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);

            return client;
        }

        public async Task<ODataResponse<T>> GetData<T>(HttpClient client, string resource, string query)
        {
            var uri = $"odata/{resource}";
            if (query != null)
            {
                uri += "?" + query;
            }

            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
#pragma warning disable SYSLIB0020 // El tipo o el miembro están obsoletos
            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                WriteIndented = true
            };
#pragma warning restore SYSLIB0020 // El tipo o el miembro están obsoletos
            var content = await response.Content.ReadAsStringAsync();
            var serializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var result = JsonConvert.DeserializeObject<ODataResponse<T>>(content, serializerSettings);
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
    }
}
