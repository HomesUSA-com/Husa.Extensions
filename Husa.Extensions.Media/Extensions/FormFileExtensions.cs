namespace Husa.Extensions.Media.Extensions
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Husa.Extensions.Media.Constants;
    using Microsoft.AspNetCore.Http;
    using SixLabors.ImageSharp;

    public static class FormFileExtensions
    {
        public const int ImageMinimumBytes = 512;
        public const string RegexExp = @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy";
        private static readonly string[] ValidContentTypes = new[] { ManagedMediaTypes.Jpg, ManagedMediaTypes.Jpeg, ManagedMediaTypes.Pjpeg, ManagedMediaTypes.Gif, ManagedMediaTypes.Xpng, ManagedMediaTypes.Png, ManagedMediaTypes.Svg, ManagedMediaTypes.Pdf };

        public static bool IsValid(this IFormFile file)
        {
            return file.ContentType.IsValidContentType() && file.IsValidSize() && file.IsValidContent() && file.IsValidStream();
        }

        private static bool IsValidContentType(this string contentType)
        {
            if (string.IsNullOrWhiteSpace(contentType))
            {
                return false;
            }

            return ValidContentTypes.Contains(contentType.ToLower());
        }

        private static bool IsValidSize(this IFormFile file)
        {
            return file.OpenReadStream().CanRead && file.Length >= ImageMinimumBytes;
        }

        private static bool IsValidContent(this IFormFile file)
        {
            try
            {
                byte[] buffer = new byte[ImageMinimumBytes];
                file.OpenReadStream().Read(buffer, 0, ImageMinimumBytes);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(
                    content,
                    RegexExp,
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }

        private static bool IsValidStream(this IFormFile file)
        {
            if (file == null)
            {
                return false;
            }

            if (file.ContentType.Equals(ManagedMediaTypes.Pdf, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            try
            {
                using var image = Image.Load(file.OpenReadStream());
            }
            catch (ArgumentException)
            {
                return false;
            }
            finally
            {
                file.OpenReadStream().Position = 0;
            }

            return true;
        }
    }
}
