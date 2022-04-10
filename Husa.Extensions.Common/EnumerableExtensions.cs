namespace Husa.Extensions.Common
{
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions
    {
        public static IEnumerable<int> ToInt(this List<string> elements)
        {
            var result = Enumerable.Empty<int>();
            if (elements.Any())
            {
                result = elements.Select(intElement =>
                {
                    _ = int.TryParse(intElement, out var intValue);
                    return intValue;
                });
            }

            return result.ToList();
        }

        public static IEnumerable<TEnum> GetListOfEnumParams<TEnum>(this List<string> enumElements)
            where TEnum : struct
        {
            var result = Enumerable.Empty<TEnum>();
            if (enumElements.Any())
            {
                result = enumElements.Select(enumElement => enumElement.GetEnumFromString<TEnum>());
            }

            return result.ToList();
        }

        public static string ToCsvString(this IEnumerable<string> elements)
        {
            if (elements == null || !elements.Any())
            {
                return string.Empty;
            }

            return string.Join(";", elements);
        }
    }
}
