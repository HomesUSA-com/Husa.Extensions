namespace Husa.Extensions.Domain.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text.RegularExpressions;
    using AutoMapper.Internal;
    using PropertyInfo = System.Reflection.PropertyInfo;

    public static class Extensions
    {
        public static string CleanPhoneValue(this string value)
        {
            var pattern = new Regex(@"[^\d]+", RegexOptions.None);
            return string.IsNullOrWhiteSpace(value) ? null : pattern.Replace(value, string.Empty).Trim();
        }

        public static IEnumerable<string> GetDifferences(this object source, object target, IEnumerable<string> exclude = null)
        {
            var properties = target.GetType().GetProperties();

            if (exclude != null)
            {
                properties = properties.Where(x => !exclude.Contains(x.Name)).ToArray();
            }

            return properties.Where(field =>
            {
                if (field.PropertyType.IsCollection())
                {
                    return !EnumCollectionsAreEquals(field, source, target);
                }

                return !Equals(field.GetValue(source), field.GetValue(target));
            }).Select(x => x.Name);
        }

        private static bool EnumCollectionsAreEquals(PropertyInfo propertyInfo, object source, object target)
        {
            var argumentType = propertyInfo.PropertyType.GetGenericArguments().Single();
            var sourceValue = EnumCollectionToStringCollection(propertyInfo.GetValue(source), argumentType);
            var targetValue = EnumCollectionToStringCollection(propertyInfo.GetValue(target), argumentType);
            if (!sourceValue.Any() && !targetValue.Any())
            {
                return true;
            }

            return sourceValue.SequenceEqual(targetValue);
        }

        private static IEnumerable<string> EnumCollectionToStringCollection(object obj, Type enumType)
        {
            if (obj == null)
            {
                return Array.Empty<string>();
            }

            var valueList = new List<string>();
            foreach (var value in (IList)obj)
            {
                valueList.Add(EnumMemberToString(enumType, value));
            }

            return valueList;
        }

        private static string EnumMemberToString(Type type, object value)
        {
            var enumType = Nullable.GetUnderlyingType(type) ?? type;
            var name = Enum.GetName(enumType, value);
            var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), inherit: true)).SingleOrDefault();

            return enumMemberAttribute != null ? enumMemberAttribute.Value : null;
        }
    }
}
