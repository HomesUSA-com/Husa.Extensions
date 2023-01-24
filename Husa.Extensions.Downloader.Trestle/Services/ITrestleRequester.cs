namespace Husa.Extensions.Downloader.Trestle.Services
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Xml;
    using Husa.Extensions.Downloader.Trestle.Models;

    public interface ITrestleRequester
    {
        Task<HttpClient> GetAuthenticatedClient();

        Task<XmlDocument> GetMetadata(HttpClient client);

        Task<ODataResponse<T>> GetData<T>(HttpClient client, string resource, string query);
    }
}
