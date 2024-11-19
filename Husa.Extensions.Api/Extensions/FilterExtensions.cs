namespace Husa.Extensions.Api.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class FilterExtensions
    {
        public static string ToQueryString<T>(this T filters)
        {
            return filters.GetType().GetProperties()
                .ToDictionary(k => k.Name, v => v.GetValue(filters))
                .Where(x => x.Value is not null)
                .Where(x => x.Value.ToString()?.Length > 0)
                .Select(x => x.Value.Parser(x.Key))
                .Join();
        }

        private static string Parser(this object obj, string name)
        {
            if (obj is not string && obj.GetType().GetInterface("IEnumerable") is not null)
            {
                return ParseList(obj, name).Join();
            }

            return obj.Parse(name);
        }

        private static string Join(this IEnumerable<string> values)
        {
            return values.Aggregate((current, next) => $"{current}&{next}");
        }

        private static string Parse<T>(this T value, string name)
        {
            return $"{name}={value}";
        }

        private static IEnumerable<string> ParseList(object obj, string name)
        {
            var type = obj.GetType();
            var getter = type.GetMethods()
                .Where(x => x.Name == "GetValue")
                .Where(x => x.GetParameters().Length == 1)
                .FirstOrDefault(x => x.GetParameters()[0].ParameterType.Equals(typeof(int)));
            var len = (int)type.GetProperty("Length")?.GetValue(obj);

            for (int i = 0; i < len && getter is not null; i++)
            {
                var value = getter.Invoke(obj, new object[] { i });
                yield return Parse(value, $"{name}[{i}]");
            }
        }
    }
}
