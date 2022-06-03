namespace Husa.Extensions.Api.Client
{
    using System.Net.Http;

#pragma warning disable S2326 // Unused type parameters should be removed
    public class HusaClient<TClient> : HusaStandardClient
    {
        public HusaClient(HttpClient httpClient)
            : base(httpClient)
        {
        }
    }
}
