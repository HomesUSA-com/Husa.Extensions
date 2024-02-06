namespace Husa.Extensions.Linq.Specifications
{
    using System.Linq;

    public static class BaseSpecifications
    {
        public static IQueryable<T> ApplyPaginationFilter<T>(this IQueryable<T> records, int skip, int take, bool avoidPagination = false)
        {
            if (avoidPagination || skip < 0 || take < 1)
            {
                return records;
            }

            return records.Skip(skip * take).Take(take);
        }
    }
}
