namespace Husa.Extensions.Linq.Specifications
{
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Reflection;

    public static class SortSpecifications
    {
        public static IOrderedQueryable<T> ApplySortByFields<T>(this IQueryable<T> records, string orderQueryString)
        {
            var attributeName = GetAttributeName<T>(orderQueryString?[1..]);
            var sortOrder = GetSortOrder(orderQueryString?.First());
            if (attributeName is not null && sortOrder is not null)
            {
                return records.OrderBy($"{attributeName} {sortOrder}");
            }

            return records as IOrderedQueryable<T>;
        }

        private static string GetSortOrder(char? sortSymbol) => sortSymbol switch
        {
            '+' => "asc",
            '-' => "desc",
            _ => null,
        };

        private static string GetAttributeName<T>(string attributeName)
        {
            if (string.IsNullOrEmpty(attributeName))
            {
                return null;
            }

            var propertyInfo = typeof(T).GetProperty(attributeName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo is not null)
            {
                return propertyInfo.Name;
            }

            return null;
        }
    }
}
