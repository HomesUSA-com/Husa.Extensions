namespace Husa.Extensions.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;

    public static class EnumExtensions
    {
        public static string GetAttributeValue<TAttribute>(this Enum enumValue, string propertyName)
            where TAttribute : Attribute
        {
            var memberInfo = enumValue.GetType().GetMember(enumValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                var attribute = memberInfo[0].GetCustomAttribute<TAttribute>(false);
                if (attribute != null)
                {
                    return attribute
                        .GetType()
                        .GetProperty(propertyName)?
                        .GetValue(attribute)?
                        .ToString()
                        ?? enumValue.ToString();
                }
            }

            return enumValue.ToString();
        }

        public static TEnum GetEnumFromString<TEnum>(this string enumElement)
            where TEnum : struct
        {
            _ = Enum.TryParse<TEnum>(enumElement, true, out var enumValue);
            return enumValue;
        }

        public static string GetEnumDescription(this Enum enumeration)
        {
            var type = enumeration.GetType();
            var memberInfo = type.GetMember(enumeration.ToString()).FirstOrDefault();

            if (memberInfo == null)
            {
                throw new InvalidOperationException($"Unable to get memberinfo for enum '{type.FullName}'");
            }

            var attribute = memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();

            return ((DescriptionAttribute)attribute)?.Description ?? throw new InvalidOperationException($"Unable to get memberinfo for enum '{type.FullName}'");
        }

        public static TEnum GetEnumValueFromDescription<TEnum>(this string enumValue, bool ignoreCase = false)
            where TEnum : struct
        {
            return enumValue.ToEnumOrNullFromDescription<TEnum>(ignoreCase: ignoreCase) ?? throw new InvalidOperationException($"Unable to get memberinfo for enum '{typeof(TEnum).FullName}'");
        }

        public static TEnum? ToEnumOrNullFromDescription<TEnum>(this string enumValue, bool ignoreCase = true)
            where TEnum : struct
        {
            var type = typeof(TEnum);
            var membersInfo = type.GetMembers();

            if (!membersInfo.Any())
            {
                throw new InvalidOperationException($"Unable to get memberinfo for enum '{type.FullName}'");
            }

            Predicate<object> predicate = ignoreCase
                ? attribute => string.Equals(((DescriptionAttribute)attribute).Description, enumValue, StringComparison.OrdinalIgnoreCase)
                : attribute => ((DescriptionAttribute)attribute).Description == enumValue;

            var memberInfo = membersInfo
                .SingleOrDefault(m => Array.Exists(m.GetCustomAttributes(typeof(DescriptionAttribute), false), predicate));

            if (memberInfo == null)
            {
                return null;
            }

            return (TEnum)Enum.Parse(typeof(TEnum), memberInfo.Name);
        }

        public static string ToEnumNameFromEnumMember<T>(this string enumMemberValue)
            where T : Enum
        {
            var enumType = typeof(T);
            foreach (var name in Enum.GetNames(enumType))
            {
                var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
                if (string.Equals(enumMemberAttribute.Value, enumMemberValue, StringComparison.OrdinalIgnoreCase))
                {
                    return name;
                }
            }

            return null;
        }

        public static T ToEnumFromEnumMember<T>(this string enumMemberValue)
            where T : Enum
        {
            var enumName = ToEnumNameFromEnumMember<T>(enumMemberValue);
            var enumType = typeof(T);
            return enumName is null ? default : (T)Enum.Parse(enumType, enumName);
        }

        public static T? ToEnumOrNullFromEnumMember<T>(this string enumMemberValue)
            where T : struct, Enum
        {
            var enumName = ToEnumNameFromEnumMember<T>(enumMemberValue);
            return enumName is null ? null : (T)Enum.Parse(typeof(T), enumName);
        }

        public static T? ToEnumOrNullFromString<T>(this string enumValue, bool ignoreCase = true)
            where T : struct, Enum
        {
            if (!string.IsNullOrEmpty(enumValue) && Enum.TryParse<T>(enumValue, ignoreCase: ignoreCase, out var result))
            {
                return result;
            }

            return null;
        }

        public static T ToEnumFromString<T>(this string enumValue)
            where T : struct, Enum
        {
            var result = (T)default;
            if (!string.IsNullOrEmpty(enumValue))
            {
                return enumValue.ToEnumOrNullFromString<T>(ignoreCase: false)
                    ?? throw new ArgumentException(string.Format("Unexpected value parsing. Expected {0} string, got {1}", typeof(T).Name, enumValue));
            }

            return result;
        }

        public static string ToStringFromEnumMember<T>(this T type, bool isOptional = false)
            where T : Enum
        {
            var enumType = typeof(T);
            var name = Enum.GetName(enumType, type);
            var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).SingleOrDefault();
            if (!isOptional)
            {
                return enumMemberAttribute?.Value;
            }

            return enumMemberAttribute != null ?
                    enumMemberAttribute.Value :
                    name;
        }

        public static string ToStringFromEnumMembers<T>(this IEnumerable<T> enumElements, bool enumMember = true)
            where T : Enum
        {
            if (enumElements is null || !enumElements.Any())
            {
                return null;
            }

            return string.Join(",", enumElements.Select(enumElement => enumMember ? enumElement.ToStringFromEnumMember() : Enum.GetName(typeof(T), enumElement)));
        }

        public static IEnumerable<string> ToStringCollectionFromEnumMembers<T>(this IEnumerable<T> enumElements)
            where T : Enum
        {
            if (enumElements is null || !enumElements.Any())
            {
                return Array.Empty<string>();
            }

            return enumElements.Select(enumElement => enumElement.ToStringFromEnumMember()).ToList();
        }

        public static IEnumerable<T> CsvToEnum<T>(this string enumElements, bool enumMember = true)
            where T : struct, Enum
            => enumElements.CsvToEnumerableEnum<T>(useEnumMember: enumMember, ignoreCase: false);

        public static IEnumerable<T> CsvToEnumerableEnum<T>(this string enumElements, bool useEnumMember, bool ignoreCase)
            where T : struct, Enum
        {
            if (string.IsNullOrWhiteSpace(enumElements))
            {
                return [];
            }

            IEnumerable<string> values = enumElements.Split(",");

            if (useEnumMember)
            {
                values = values.Select(feature => feature.ToEnumNameFromEnumMember<T>());
            }

            return values.Select(feature => feature.ToEnumOrNullFromString<T>(ignoreCase: ignoreCase))
                .Where(name => name.HasValue)
                .Select(name => name.Value);
        }

        public static TEnum? GetEnumFromText<TEnum>(this string enumValuetoFind)
            where TEnum : struct
        {
            if (string.IsNullOrWhiteSpace(enumValuetoFind))
            {
                throw new ArgumentException($"'{nameof(enumValuetoFind)}' cannot be null or whitespace.", nameof(enumValuetoFind));
            }

            var enumType = typeof(TEnum);
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException($"The type {enumType.Name} must be an enum");
            }

            var valueWithoutSpaces = enumValuetoFind.RemoveWhiteSpaces();

            if (Enum.TryParse<TEnum>(valueWithoutSpaces, ignoreCase: true, out var enumValue))
            {
                return enumValue;
            }

            var enumMembers = enumType.GetMembers();

            var memberInfo = enumMembers.SingleOrDefault(member => Array.Find(GetDescriptionAttributes(member), (DescriptionAttribute attribute) => attribute.Description.EqualsTo(valueWithoutSpaces)) != null)
                ?? Array.Find(enumMembers, member => Array.Find(GetDescriptionAttributes(member), (DescriptionAttribute attribute) => attribute.Description.Contains(valueWithoutSpaces, StringComparison.InvariantCultureIgnoreCase)) != null)
                ?? Array.Find(enumMembers, member => Array.Find(GetDescriptionAttributes(member), (DescriptionAttribute attribute) => enumValuetoFind.Contains(attribute.Description, StringComparison.InvariantCultureIgnoreCase)) != null)
                ?? Array.Find(enumMembers, member => Array.Find(GetEnumMemberAttributes(member), (EnumMemberAttribute attribute) => attribute.Value.EqualsTo(valueWithoutSpaces)) != null)
                ?? Array.Find(enumMembers, member => Array.Find(GetEnumMemberAttributes(member), (EnumMemberAttribute attribute) => attribute.Value.Contains(valueWithoutSpaces)) != null)
                ?? Array.Find(enumMembers, member => Array.Find(GetEnumMemberAttributes(member), (EnumMemberAttribute attribute) => enumValuetoFind.Contains(attribute.Value, StringComparison.InvariantCultureIgnoreCase)) != null);

            return memberInfo != null ? (TEnum)Enum.Parse(typeof(TEnum), memberInfo.Name) : null;

            DescriptionAttribute[] GetDescriptionAttributes(System.Reflection.MemberInfo member)
            {
                return member.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false).Select(attribute => (DescriptionAttribute)attribute).ToArray();
            }

            EnumMemberAttribute[] GetEnumMemberAttributes(System.Reflection.MemberInfo member)
            {
                return member.GetCustomAttributes(typeof(EnumMemberAttribute), inherit: false).Select(attribute => (EnumMemberAttribute)attribute).ToArray();
            }
        }
    }
}
