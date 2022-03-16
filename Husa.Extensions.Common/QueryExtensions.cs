namespace Husa.Extensions.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.WebUtilities;

    public static class QueryExtensions
    {
        public static string AddQueryString<T>(this string uri, string name, IEnumerable<T> value)
        {
            var hasValue = value != null && value.Any() && value.All(a => a != null);
            return hasValue
              ? value.Aggregate(uri, (current, val) => QueryHelpers.AddQueryString(current, name, val.ToString()))
              : uri;
        }

        public static string AddQueryString<T>(this string uri, string name, T value)
        {
            return value == null ? uri : QueryHelpers.AddQueryString(uri, name, value.ToString());
        }

        public static string AddQueryString<T>(this string uri, string name, T value, bool addFilter)
        {
            return value == null || !addFilter ? uri : QueryHelpers.AddQueryString(uri, name, value.ToString());
        }
    }
}
