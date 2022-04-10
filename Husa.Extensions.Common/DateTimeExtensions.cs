namespace Husa.Extensions.Common
{
    using System;

    public static class DateTimeExtensions
    {
        public static string ToFileNameFormat(this DateTime dateTime)
        {
            return dateTime.ToString("MM-dd-yyyy_HHmmss");
        }
    }
}
