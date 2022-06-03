namespace Husa.Extensions.Common
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;
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

        public static bool EqualsTo(this string leftStr, string rightStr) => !string.IsNullOrEmpty(leftStr) && leftStr.Equals(rightStr, StringComparison.InvariantCultureIgnoreCase);

        public static string RemoveNonNumericCharacters(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            return Regex.Replace(str, "[^0-9]+", string.Empty, RegexOptions.Compiled);
        }
    }
}
