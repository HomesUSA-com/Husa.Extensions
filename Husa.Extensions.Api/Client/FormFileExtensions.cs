namespace Husa.Extensions.Api.Client
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Net.Mime;
    using System.Text.RegularExpressions;
    using Microsoft.AspNetCore.Http;

    public static class FormFileExtensions
    {
        public const int ImageMinimumBytes = 512;
        public const string RegexExp = @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy";
        private static readonly string[] ValidContentTypes = new[] { "image/jpg", MediaTypeNames.Image.Jpeg, "image/pjpeg", MediaTypeNames.Image.Gif, "image/x-png", "image/png", MediaTypeNames.Application.Pdf };
        private static readonly string[] ValidFileExtensions = new[] { ".jpg", ".png", ".gif", ".jpeg", ".pdf" };

        public static bool IsValid(this IFormFile file)
        {
            return file.ContentType.IsValidContentType() && Path.GetExtension(file.FileName).IsValidExtension() && file.IsValidSize() && file.IsValidContent() && file.IsValidStream();
        }

        private static bool IsValidContentType(this string contentType)
        {
            if (string.IsNullOrWhiteSpace(contentType))
            {
                return false;
            }

            return ValidContentTypes.Contains(contentType.ToLower());
        }

        private static bool IsValidExtension(this string extension)
        {
            if (string.IsNullOrWhiteSpace(extension))
            {
                return false;
            }

            return ValidFileExtensions.Contains(extension.ToLower());
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

            if (file.ContentType.Equals(MediaTypeNames.Application.Pdf, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            try
            {
                using var bitmap = new Bitmap(file.OpenReadStream());
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
