namespace Husa.Extensions.Document.Repositories
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Husa.Extensions.Document.Interfaces;
    using Husa.Extensions.Document.Interfaces.Repositories;
    using Microsoft.Azure.Cosmos;

    public abstract class DocumentRepository<TDocument> : DocumentQueriesRepository<TDocument>, IDocumentRepository<TDocument>
        where TDocument : class, IDocument
    {
        protected DocumentRepository(
            CosmosClient cosmosClient,
            ICosmosLinqQuery cosmosLinqQuery,
            string databaseName,
            string collectionName)
            : base(cosmosClient, cosmosLinqQuery, databaseName, collectionName)
        {
        }

        public virtual Task AddDocumentAsync(TDocument document, CancellationToken cancellationToken = default)
        {
            return this.DbContainer.CreateItemAsync(document, new PartitionKey(document.EntityId.ToString()), cancellationToken: cancellationToken);
        }

        public virtual Task UpdateDocumentAsync(Guid documentId, TDocument document, Guid userId, CancellationToken cancellationToken = default)
        {
            document.UpdateTrackValues(userId);
            var resp = this.DbContainer.ReplaceItemAsync(document, documentId.ToString(), cancellationToken: cancellationToken);
            return resp;
        }
    }
}
