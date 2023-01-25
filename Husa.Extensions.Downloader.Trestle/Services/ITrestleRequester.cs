namespace Husa.Extensions.Downloader.Trestle.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Xml;

    public interface ITrestleRequester
    {
        Task<HttpClient> GetAuthenticatedClient();

        Task<XmlDocument> GetMetadata(HttpClient client);

        Task<IEnumerable<T>> GetData<T>(HttpClient client, string resource, string filter);
    }
}
