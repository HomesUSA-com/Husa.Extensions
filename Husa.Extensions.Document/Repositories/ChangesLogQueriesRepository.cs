namespace Husa.Extensions.Document.Repositories
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Husa.Extensions.Document.Interfaces;
    using Husa.Extensions.Document.Models;
    using Husa.Extensions.Document.QueryFilters;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Linq;

    public class ChangesLogQueriesRepository : ContainerRepository
    {
        public ChangesLogQueriesRepository(
            CosmosClient cosmosClient,
            ICosmosLinqQuery cosmosLinqQuery,
            string databaseName,
            string collectionName)
            : base(cosmosClient, cosmosLinqQuery, databaseName, collectionName)
        {
        }

        public virtual async Task<DocumentGridQueryResult<ChangesLogQueryResult>> GetAsync(RequestBaseQueryFilter queryFilter, CancellationToken cancellationToken = default)
        {
            var queryRequestOptions = new QueryRequestOptions() { MaxItemCount = queryFilter.Take };
            var continuationToken = queryFilter.IsPrint ? null : queryFilter.ContinuationToken;

            var query = this.FilterByQueryFilter(this.DbContainer.GetItemLinqQueryable<ChangesLogQueryResult>(false, continuationToken, queryRequestOptions), queryFilter);

            var queryCount = await this.FilterByQueryFilter(this.DbContainer.GetItemLinqQueryable<ChangesLogQueryResult>(false), queryFilter)
                .CountAsync(cancellationToken);

            var requests = await this.ReadDocumentFeedToGrid(query, continuationToken, cancellationToken: cancellationToken);
            requests.Total = queryCount;

            return requests;
        }

        public virtual Task<SavedChangesLog> GetByIdAsync(Guid documentId, CancellationToken cancellationToken = default)
        {
            var query = this.DbContainer
                .GetItemLinqQueryable<SavedChangesLog>()
                .Where(x => x.Id == documentId);

            return this.ReadFirstDocument(query, cancellationToken);
        }

        protected virtual IQueryable<ChangesLogQueryResult> FilterByQueryFilter(IQueryable<ChangesLogQueryResult> records, RequestBaseQueryFilter queryFilter)
        {
            if (queryFilter.EntityId.HasValue)
            {
                records = records.Where(x => x.EntityId == queryFilter.EntityId.Value);
            }

            return records;
        }
    }
}
