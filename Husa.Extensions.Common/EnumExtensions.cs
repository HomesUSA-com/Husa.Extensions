namespace Husa.Extensions.Common
{
    using System;
    using System.ComponentModel;
    using System.Linq;

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
    }
}
