namespace Husa.Extensions.Common
{
    using System;

    public static class DateTimeExtensions
    {
        public const string CentralStandardTime = "Central Standard Time";

        public static string ToFileNameFormat(this DateTime dateTime)
        {
            return dateTime.ToString("MM-dd-yyyy_HHmmss");
        }

        public static DateTime ToCSTDateTime(this DateTime dateTime)
        {
            var cstZone = TimeZoneInfo.FindSystemTimeZoneById(CentralStandardTime);
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, cstZone);
        }

        public static string ToCSTString(this DateTime dateTime)
        {
            return dateTime
                .ToCSTDateTime()
                .ToString("s");
        }

        public static DateTime ToTimeZoneDateTime(this DateTime dateTime, string timeZoneId)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZone);
        }

        public static string ToTimeZoneString(this DateTime dateTime, string timeZoneId)
        {
            return dateTime
                .ToTimeZoneDateTime(timeZoneId)
                .ToString("s");
        }

        public static DateTime ToUtc(this DateTime date)
        {
            TimeZoneInfo cstTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            return TimeZoneInfo.ConvertTimeToUtc(date, cstTimeZone);
        }

        public static DateTime? ToUtc(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToUtc() : null;
        }
    }
}
