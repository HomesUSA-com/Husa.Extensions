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

        public static void CopyProperties(this object target, IEnumerable<string> include)
        {
            if (target == null || include == null || !include.Any())
            {
                return;
            }

            Type sourceType = target.GetType();
            Type targetType = target.GetType();
            var sourceProperties = sourceType.GetProperties().Where(p => include.Contains(p.Name));
            foreach (var property in sourceProperties)
            {
                var targetProperty = targetType.GetProperty(property.Name);
                if (targetProperty != null && targetProperty.CanWrite)
                {
                    var sourceValue = property.GetValue(target);
                    targetProperty.SetValue(target, sourceValue, null);
                }
            }
        }

        public static IEnumerable<string> EnumCollectionToStringCollection(object obj, Type enumType)
        {
            if (obj == null)
            {
                return Array.Empty<string>();
            }

            var valueList = new List<string>();
            foreach (var value in (IList)obj)
            {
                if (enumType.IsEnum)
                {
                    valueList.Add(EnumMemberToString(enumType, value));
                }
                else
                {
                    valueList.Add((string)value);
                }
            }

            return valueList;
        }

        public static string EnumMemberToString(Type type, object value)
        {
            var enumType = Nullable.GetUnderlyingType(type) ?? type;
            var name = Enum.GetName(enumType, value);
            var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), inherit: true)).SingleOrDefault();

            return enumMemberAttribute != null ? enumMemberAttribute.Value : null;
        }

        public static string CleanAfterKeyword(this string value, string keyword)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            int index = value.IndexOf(keyword, StringComparison.OrdinalIgnoreCase);

            if (index != -1)
            {
                return value.Substring(0, index).Trim();
            }

            return value;
        }

        public static string CleanForComparison(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            value = System.Text.RegularExpressions.Regex.Replace(value, @"\s", string.Empty);
            value = value.Replace(".", string.Empty);
            value = value.Replace("\"", string.Empty);
            value = value.Replace("'", string.Empty);
            value = value.Replace("’", string.Empty);

            return value;
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
    }
}
