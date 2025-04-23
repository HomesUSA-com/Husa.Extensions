namespace Husa.Extensions.Common
{
    using System;
    using Husa.Extensions.Common.Enums;

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
            if (date.Kind == DateTimeKind.Utc)
            {
                return date;
            }

            if (date.Kind == DateTimeKind.Local)
            {
                var offsetHours = date.GetOffsetHours();
                var newDate = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, DateTimeKind.Utc);
                return newDate.AddHours(-offsetHours);
            }

            TimeZoneInfo cstTimeZone;
            try
            {
                // Windows timezone ID
                cstTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            }
            catch
            {
                // Linux/macOS timezone ID
                cstTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Chicago");
            }

            return TimeZoneInfo.ConvertTimeToUtc(date, cstTimeZone);
        }

        public static DateTime? ToUtc(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToUtc() : null;
        }

        public static DateTime TodayUtc()
        {
            var offsetHours = DateTime.Now.GetOffsetHours();
            return DateTime.UtcNow.Date.AddHours(-offsetHours);
        }

        public static bool DateCompare(this DateTime? date, OperatorType conditionOperator, DateTime? dateToCompare, bool compareTime = false)
            => date.HasValue && date.Value.DateCompare(conditionOperator, dateToCompare, compareTime);

        public static bool DateCompare(this DateTime date, OperatorType conditionOperator, DateTime? dateToCompare, bool compareTime = true)
        {
            if (!dateToCompare.HasValue)
            {
                return false;
            }

            var dateUtc = date.ToUtc();
            var dateToCompareUtc = dateToCompare.Value.ToUtc();

            var auxDate = compareTime ? dateUtc : dateUtc.Date;
            var auxDateToCompare = compareTime ? dateToCompareUtc : dateToCompareUtc.Date;

            switch (conditionOperator)
            {
                case OperatorType.LessThan:
                    return auxDate < auxDateToCompare;
                case OperatorType.LessEqual:
                    return auxDate <= auxDateToCompare;
                case OperatorType.GreaterThan:
                    return auxDate > auxDateToCompare;
                case OperatorType.GreaterEqual:
                    return auxDate >= auxDateToCompare;
                case OperatorType.Equal:
                    return auxDate == auxDateToCompare;
                case OperatorType.NotEqual:
                    return auxDate != auxDateToCompare;
                default:
                    return false;
            }
        }

        public static int GetOffsetHours(this DateTime date)
        {
            TimeSpan offset = TimeZoneInfo.Local.GetUtcOffset(date);
            return (int)offset.TotalHours;
        }
    }
}
