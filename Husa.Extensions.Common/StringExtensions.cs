namespace Husa.Extensions.Common
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Microsoft.AspNetCore.WebUtilities;

    public static class StringExtensions
    {
        private static readonly IEnumerable<string> ExcludedWords = new[] { "a", "an", "and", "at", "in", "of", "or", "the", "to", "with" };

        public static string ToTitleCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            var textInfo = CultureInfo.CurrentCulture.TextInfo;
            var words = str.Split(' ');

            words = words.Select(word =>
            {
                var loweredWord = word.ToLower();
                return ExcludedWords.Contains(loweredWord) ? loweredWord : textInfo.ToTitleCase(loweredWord);
            }).ToArray();

            return string.Join(" ", words);
        }

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

        public static string RemoveWhiteSpaces(this string str)
        {
            var inputAsArray = str.ToCharArray();
            int characterNewIndex = 0;
            for (int index = 0; index < str.Length; index++)
            {
                var inputCharacter = inputAsArray[index];
                switch (inputCharacter)
                {
                    case '\u0020':
                    case '\u00A0':
                    case '\u1680':
                    case '\u2000':
                    case '\u2001':
                    case '\u2002':
                    case '\u2003':
                    case '\u2004':
                    case '\u2005':
                    case '\u2006':
                    case '\u2007':
                    case '\u2008':
                    case '\u2009':
                    case '\u200A':
                    case '\u202F':
                    case '\u205F':
                    case '\u3000':
                    case '\u2028':
                    case '\u2029':
                    case '\u0009':
                    case '\u000A':
                    case '\u000B':
                    case '\u000C':
                    case '\u000D':
                    case '\u0085':
                        continue;
                    default:
                        inputAsArray[characterNewIndex++] = inputCharacter;
                        break;
                }
            }

            return new string(inputAsArray, startIndex: 0, characterNewIndex);
        }

        public static ICollection<string> ToCollectionFromString(this string enumElements, string separator = ",")
        {
            if (string.IsNullOrWhiteSpace(enumElements))
            {
                return Array.Empty<string>();
            }

            return enumElements.Split(separator).ToList();
        }

        public static string ToStringFromCollection(this ICollection<string> enumElements, string separator = ",")
        {
            if (enumElements == null || !enumElements.Any())
            {
                return null;
            }

            return string.Join(separator, enumElements);
        }
    }
}
