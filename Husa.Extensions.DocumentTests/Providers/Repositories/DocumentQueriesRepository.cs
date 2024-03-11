namespace Husa.Extensions.Document.Tests.Providers.Repositories
{
    using Husa.Extensions.Document.Interfaces;
    using Husa.Extensions.Document.Tests.Configuration;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.Options;
    using DocumentRepositories = Husa.Extensions.Document.Repositories;

    public class DocumentQueriesRepository : DocumentRepositories.DocumentQueriesRepository<IDocument>
    {
        public DocumentQueriesRepository(CosmosClient cosmosClient, ICosmosLinqQuery cosmosLinqQuery, IOptions<DocumentDbSettings> options)
            : base(cosmosClient, cosmosLinqQuery, options.Value.DatabaseName, "collection")
        {
        }
    }
}
