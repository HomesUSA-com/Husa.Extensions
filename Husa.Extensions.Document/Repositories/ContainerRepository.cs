namespace Husa.Extensions.Document.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Husa.Extensions.Document.Interfaces;
    using Husa.Extensions.Document.Models;
    using Microsoft.Azure.Cosmos;

    public abstract class ContainerRepository
    {
        protected ContainerRepository(
            CosmosClient cosmosClient,
            ICosmosLinqQuery cosmosLinqQuery,
            string databaseName,
            string collectionName)
        {
            if (cosmosClient is null)
            {
                throw new ArgumentNullException(nameof(cosmosClient));
            }

            this.DbContainer = cosmosClient.GetContainer(databaseName, collectionName);
            this.CosmosLinqQuery = cosmosLinqQuery ?? throw new ArgumentNullException(nameof(cosmosLinqQuery));
        }

        protected Container DbContainer { get; }

        protected ICosmosLinqQuery CosmosLinqQuery { get; }

        protected async Task<IEnumerable<T>> ReadDocumentFeedToEnumerable<T>(IQueryable<T> query, CancellationToken cancellationToken = default)
           where T : class
        {
            var results = new List<T>();
            using (var queryIterator = this.CosmosLinqQuery.GetFeedIterator(query))
            {
                if (queryIterator.HasMoreResults)
                {
                    var queryResult = await queryIterator.ReadNextAsync(cancellationToken);
                    results.AddRange(queryResult.ToList());
                }
            }

            return results;
        }

        protected async Task<T> ReadFirstDocument<T>(IQueryable<T> query, CancellationToken cancellationToken = default)
            where T : class
        {
            using (var queryIterator = this.CosmosLinqQuery.GetFeedIterator(query))
            {
                if (queryIterator.HasMoreResults)
                {
                    var nextFeedIterator = await queryIterator.ReadNextAsync(cancellationToken);
                    return nextFeedIterator.FirstOrDefault();
                }
            }

            return null;
        }

        protected virtual async Task<DocumentGridQueryResult<T>> ReadDocumentFeedToGrid<T>(IQueryable<T> query, string continuationToken = null, CancellationToken cancellationToken = default)
            where T : class
        {
            var results = new List<T>();
            using (var feedIterator = this.CosmosLinqQuery.GetFeedIterator(query))
            {
                if (feedIterator.HasMoreResults)
                {
                    var queryResults = await feedIterator.ReadNextAsync(cancellationToken);
                    continuationToken = queryResults.ContinuationToken;
                    results.AddRange(queryResults.ToList());
                }
            }

            return new(results, continuationToken);
        }
    }
}
