namespace Husa.Extensions.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.Serialization;

    public static class EnumExtensions
    {
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

        public static TEnum GetEnumValueFromDescription<TEnum>(this string enumValue)
            where TEnum : struct
        {
            var type = typeof(TEnum);
            var membersInfo = type.GetMembers();

            if (!membersInfo.Any())
            {
                throw new InvalidOperationException($"Unable to get memberinfo for enum '{type.FullName}'");
            }

            var memberInfo = membersInfo
                .SingleOrDefault(m => m
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .FirstOrDefault(attribute => ((DescriptionAttribute)attribute).Description == enumValue) != null);

            if (memberInfo == null)
            {
                throw new InvalidOperationException($"Unable to get memberinfo for enum '{type.FullName}'");
            }

            return (TEnum)Enum.Parse(typeof(TEnum), memberInfo.Name);
        }

        public static T ToEnumFromEnumMember<T>(this string enumMemberValue)
            where T : Enum
        {
            var enumType = typeof(T);
            var result = Enum.GetNames(enumType)
            .Where(enumName =>
                ((EnumMemberAttribute[])enumType.GetField(enumName).GetCustomAttributes(typeof(EnumMemberAttribute), inherit: true))
                .Single()
                .Value == enumMemberValue)
            .Select(enumName => (T)Enum.Parse(enumType, enumName))
            .SingleOrDefault();

            return result is not null ? result : default;
        }

        public static string ToStringFromEnumMember<T>(this T type)
            where T : Enum
        {
            var enumType = typeof(T);
            var name = Enum.GetName(enumType, type);
            var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
            return enumMemberAttribute.Value;
        }

        public static string ToStringFromEnumMembers<T>(this IEnumerable<T> enumElements)
            where T : Enum
        {
            if (enumElements is null || !enumElements.Any())
            {
                return null;
            }

            return string.Join(",", enumElements.Select(garageFeature => garageFeature.ToStringFromEnumMember()));
        }

        public static IEnumerable<T> CsvToEnum<T>(this string enumElements)
            where T : Enum
        {
            if (string.IsNullOrWhiteSpace(enumElements))
            {
                return Array.Empty<T>();
            }

            return enumElements
                .Split(",")
                .Select(garageFeature => garageFeature.ToEnumFromEnumMember<T>());
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
            var memberInfo = enumMembers
                .SingleOrDefault(member => member
                    .GetCustomAttributes(typeof(DescriptionAttribute), inherit: false)
                    .FirstOrDefault(attribute => IsValueFound(valueToCompare: ((DescriptionAttribute)attribute).Description, enumValuetoFind, valueWithoutSpaces)) != null);

            if (memberInfo != null)
            {
                return (TEnum)Enum.Parse(typeof(TEnum), memberInfo.Name);
            }

            memberInfo = enumMembers
                .SingleOrDefault(member => member
                    .GetCustomAttributes(typeof(EnumMemberAttribute), inherit: false)
                    .FirstOrDefault(attribute => IsValueFound(valueToCompare: ((EnumMemberAttribute)attribute).Value, enumValuetoFind, valueWithoutSpaces)) != null);

            return memberInfo != null ? (TEnum)Enum.Parse(typeof(TEnum), memberInfo.Name) : null;
        }

        private static bool IsValueFound(string valueToCompare, string enumValuetoFind, string valueWithoutSpaces)
        {
            return valueToCompare.EqualsTo(valueWithoutSpaces) ||
            valueToCompare.Contains(valueWithoutSpaces, StringComparison.InvariantCultureIgnoreCase) ||
            enumValuetoFind.Contains(valueToCompare, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
