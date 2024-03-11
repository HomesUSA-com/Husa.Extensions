namespace Husa.Extensions.Document.Services
{
    using System.Linq;
    using Husa.Extensions.Document.Interfaces;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Linq;

    public class CosmosLinqQuery : ICosmosLinqQuery
    {
        public FeedIterator<T> GetFeedIterator<T>(IQueryable<T> query) => query.ToFeedIterator();
    }
}
