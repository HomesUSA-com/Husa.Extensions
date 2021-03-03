namespace Husa.Extensions.Common
{
    using System;

    public static class EnumExtensions
    {
        public static TEnum GetEnumFromString<TEnum>(this string enumElement)
            where TEnum : struct
        {
            _ = Enum.TryParse<TEnum>(enumElement, true, out var enumValue);
            return enumValue;
        }
    }
}
