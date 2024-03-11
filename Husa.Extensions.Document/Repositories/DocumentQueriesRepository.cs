namespace Husa.Extensions.Document.Repositories
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Husa.Extensions.Document.Interfaces;
    using Husa.Extensions.Document.Models;
    using Husa.Extensions.Document.Specifications;
    using Husa.Extensions.Document.Specifications.Document;
    using Husa.Extensions.Document.ValueObjects;
    using Microsoft.Azure.Cosmos;

    public abstract class DocumentQueriesRepository<TDocument> : ContainerRepository
        where TDocument : class, IDocument
    {
        protected DocumentQueriesRepository(
            CosmosClient cosmosClient,
            ICosmosLinqQuery cosmosLinqQuery,
            string databaseName,
            string collectionName)
            : base(cosmosClient, cosmosLinqQuery, databaseName, collectionName)
        {
        }

        public virtual Task<TDocument> GetByIdAsync(Guid documentId, CancellationToken cancellationToken = default)
        {
            var query = this.DbContainer
                .GetItemLinqQueryable<TDocument>()
                .FilterById(documentId);

            return this.ReadFirstDocument(query, cancellationToken);
        }

        public virtual Task<TDocument> GetByLegacyIdAsync(int legacyId, CancellationToken cancellationToken = default)
        {
            var query = this.DbContainer
                .GetItemLinqQueryable<TDocument>()
                .Where(x => x.LegacyId == legacyId);

            return this.ReadFirstDocument(query, cancellationToken);
        }

        public virtual Task<TDocument> GetLastRecordAsync(Guid entityId, DateTime? limitDate = null, CancellationToken cancellationToken = default)
        {
            var query = this.DbContainer
                    .GetItemLinqQueryable<TDocument>(false)
                    .FilterByNonDeleted()
                    .FilterByEntityId(entityId);

            if (limitDate.HasValue)
            {
                query = query.Where(request => request.SysCreatedOn < limitDate.Value);
            }

            var queryOrder = query.OrderByDescending(x => x.SysCreatedOn);

            return this.ReadFirstDocument(queryOrder, cancellationToken);
        }

        protected virtual SummarySectionQueryResult TransformSummaryToQueryResult(SummarySection item)
        {
            return new SummarySectionQueryResult
            {
                Name = item.Name,
                Fields = item.Fields.Select(summaryField => new SummaryFieldQueryResult
                {
                    FieldName = summaryField.FieldName,
                    NewValue = summaryField.NewValue,
                    OldValue = summaryField.OldValue,
                }),
            };
        }
    }
}
