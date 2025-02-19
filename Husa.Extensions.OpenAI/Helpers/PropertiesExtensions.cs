namespace Husa.Extensions.OpenAI.Helpers
{
    using System.Collections.Generic;
    using System.Linq;

    public static class PropertiesExtensions
    {
        public static bool IsEmptyCollections(this object propertyValue)
            => propertyValue == null || (propertyValue is IEnumerable<string> enumerable && !enumerable.Any());

        public static bool IsNullOrEmpty(this object propertyValue)
            => propertyValue == null || (propertyValue is string strValue && string.IsNullOrWhiteSpace(strValue));

        public static bool IsNullOrZero(this object propertyValue)
        {
            if (propertyValue == null)
            {
                return true;
            }

            if (propertyValue is decimal?)
            {
                return (propertyValue as decimal?).IsNullOrZero();
            }

            if (propertyValue is int?)
            {
                return (propertyValue as int?).IsNullOrZero();
            }

            if (propertyValue is double?)
            {
                return (propertyValue as double?).IsNullOrZero();
            }

            return false;
        }

        private static bool IsNullOrZero(this int? value)
            => !value.HasValue || value.Value == 0;

        private static bool IsNullOrZero(this decimal? value)
            => !value.HasValue || value.Value == 0m;

        private static bool IsNullOrZero(this double? value)
            => !value.HasValue || value.Value == 0d;
    }
}
