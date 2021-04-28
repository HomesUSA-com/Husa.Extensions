namespace Husa.Extensions.Common
{
    using System;
    using System.Text;
    using Microsoft.AspNetCore.WebUtilities;

    public static class StringExtensions
    {
        public static string EncodeToBase64Url(this string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        }

        public static string DecodeFromBase64Url(this string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            return Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
        }

        public static bool EqualsTo(this string leftStr, string rightStr)
        {
            return !string.IsNullOrEmpty(leftStr) ? leftStr.Equals(rightStr, StringComparison.InvariantCultureIgnoreCase) : false;
        }
    }
}
