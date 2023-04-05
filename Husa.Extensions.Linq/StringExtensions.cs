namespace Husa.Quicklister.Sabor.Data.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class StringExtensions
    {
        public static ICollection<string> ToCollectionFromString(this string enumElements, string separator = ",")
        {
            if (string.IsNullOrWhiteSpace(enumElements))
            {
                return Array.Empty<string>();
            }

            return enumElements.Split(separator).ToList();
        }

        public static string ToStringFromCollection(this ICollection<string> enumElements, string separator = ",")
        {
            if (enumElements == null || !enumElements.Any())
            {
                return null;
            }

            return string.Join(separator, enumElements);
        }
    }
}
