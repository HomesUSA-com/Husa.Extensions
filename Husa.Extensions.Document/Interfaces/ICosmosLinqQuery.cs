namespace Husa.Extensions.Document.Interfaces
{
    using System.Linq;
    using Microsoft.Azure.Cosmos;
    public interface ICosmosLinqQuery
    {
        FeedIterator<T> GetFeedIterator<T>(IQueryable<T> query);
    }
}
