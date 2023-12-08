namespace Husa.Extensions.Downloader.Trestle.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Xml;
    using Husa.Extensions.Downloader.Trestle.Contracts;
    using Husa.Extensions.Downloader.Trestle.Models;

    public interface ITrestleRequester
    {
        HttpClient GetAuthenticatedClient(string accessToken);

        Task<AuthenticationResult> GetTokenInfo(AuthInfo authInfo);

        Task<XmlDocument> GetMetadata(HttpClient client);

        Task<IEnumerable<T>> GetData<T>(HttpClient client, string resource, string filter = null);

        Task<Stream> GetMediaStream(HttpClient client, string entityKey);
    }
}
