namespace Husa.Extensions.Downloader.Trestle.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Xml;
    using Husa.Extensions.Downloader.Trestle.Contracts;

    public interface ITrestleRequester
    {
        HttpClient GetAuthenticatedClient(string accessToken);

        Task<HttpClient> GetAuthenticatedClient();

        Task<AuthenticationResult> GetTokenInfo();

        Task<XmlDocument> GetMetadata(HttpClient client);

        Task<IEnumerable<T>> GetData<T>(HttpClient client, string resource, string filter = null);
    }
}
