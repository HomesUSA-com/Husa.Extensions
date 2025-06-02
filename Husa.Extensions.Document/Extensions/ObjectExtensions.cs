namespace Husa.Extensions.Document.Extensions
{
    using System.Collections;
    using System.Linq;
    using System.Text.Json;

    public static class ObjectExtensions
    {
        private static readonly JsonNamingPolicy CamelCasePolicy = JsonNamingPolicy.CamelCase;

        public static object TransformEnumToString(this object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is IDictionary dictionary)
            {
                var keys = new ArrayList(dictionary.Keys);

                foreach (var key in keys)
                {
                    var dictValue = dictionary[key];
                    dictionary[key] = dictValue.TransformEnumToString();
                }

                return dictionary;
            }

            switch (value)
            {
                case IEnumerable:
                    {
                        var typeArguments = value.GetType().GetGenericArguments();
                        if (typeArguments.Length < 1 || !typeArguments[0].IsEnum)
                        {
                            return value;
                        }

                        return ((IEnumerable)value).Cast<object>().Select(e => e.ToCamelCase()).ToList();
                    }

                default:
                    return value.GetType().IsEnum ? value.ToCamelCase() : value;
            }
        }

        private static string ToCamelCase(this object obj)
            => obj?.ToString().ToCamelCase();

        private static string ToCamelCase(this string str)
            => string.IsNullOrEmpty(str) ? str : CamelCasePolicy.ConvertName(str);
    }
}
