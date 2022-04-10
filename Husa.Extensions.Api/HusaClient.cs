namespace Husa.Extensions.Api
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

#pragma warning disable S2326 // This warning is temporarily disabled until the need for the generic type is defined
    public class HusaClient<TClient>
#pragma warning restore S2326 // Unused type parameters should be removed
        where TClient : class
    {
        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions options;

        public HusaClient(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.options = new JsonSerializerOptions().SetConfiguration();
        }

        public async Task<T> GetAsync<T>(string url, CancellationToken token = default)
        {
            using var httpResponse = await this.httpClient.GetAsync(url, token);
            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content.ReadFromJsonAsync<T>(this.options, cancellationToken: token);
        }

        public async Task DeleteAsync(string url, CancellationToken token = default)
        {
            var httpResponse = await this.httpClient.DeleteAsync(url, cancellationToken: token);
            httpResponse.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync<T>(string url, T dataObject, CancellationToken token = default)
        {
            var serializedDataObject = JsonSerializer.Serialize(dataObject);
            var content = new StringContent(serializedDataObject, Encoding.UTF8, "application/json");
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = content,
            };
            var httpResponse = await this.httpClient.SendAsync(httpRequestMessage, token);
            httpResponse.EnsureSuccessStatusCode();
        }

        public async Task PostAsJsonAsync<T>(string url, T dataObject, CancellationToken token = default)
        {
            var httpResponse = await this.httpClient.PostAsJsonAsync(url, dataObject, cancellationToken: token);
            httpResponse.EnsureSuccessStatusCode();
        }

        public async Task<TResult> PostAsJsonAsync<T, TResult>(string url, T dataObject, CancellationToken token = default)
        {
            var httpResponse = await this.httpClient.PostAsJsonAsync(url, dataObject, cancellationToken: token);
            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content.ReadFromJsonAsync<TResult>(this.options, cancellationToken: token);
        }

        public async Task<TResult> PostAsync<TResult>(string url, HttpContent content, CancellationToken token = default)
        {
            using var httpResponse = await this.httpClient.PostAsync(url, content, cancellationToken: token);
            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content.ReadFromJsonAsync<TResult>(this.options, cancellationToken: token);
        }

        public async Task PutAsJsonAsync<T>(string url, T dataObject, CancellationToken token = default)
        {
            var httpResponse = await this.httpClient.PutAsJsonAsync(url, dataObject, cancellationToken: token);
            httpResponse.EnsureSuccessStatusCode();
        }
    }
}
