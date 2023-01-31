namespace Husa.Extensions.Downloader.Trestle.Helpers
{
    using System.Collections.Generic;
    using System.Linq;

    public static class QueryStringExtensions
    {
        public static string AddQueryStrings(this string uri, string name, IEnumerable<string> value)
        {
            var valueString = string.Join("and ", value.Where(s => !string.IsNullOrEmpty(s)));
            return string.IsNullOrEmpty(valueString) ? uri : uri + $"&${name}={valueString}";
        }

        public static string AddQueryString(this string uri, string name, string value, bool addFilter)
        {
            return string.IsNullOrEmpty(value) || !addFilter ? uri : uri + $"&${name}={value}";
        }
    }
}
