namespace Husa.Extensions.Media.Extensions
{
    using System;
    using System.IO;
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

        public static IFormFile ToStandarFileFormat(this IFormFile file)
            => file?.ContentType.StartsWith("image/", StringComparison.CurrentCultureIgnoreCase) ?? false ? file.ToJpeg() : file;

        public static IFormFile ToJpeg(this IFormFile file)
        {
            var isImage = file?.ContentType.StartsWith("image/", StringComparison.CurrentCultureIgnoreCase) ?? false;
            var isJpeg = Regex.IsMatch(file?.ContentType ?? string.Empty, "(jpg|jpeg)", RegexOptions.IgnoreCase);
            if (isImage && !isJpeg)
            {
                var outputStream = new MemoryStream();
                using (var memoryString = new MemoryStream())
                {
                    file.CopyTo(memoryString);
                    memoryString.Position = 0;
                    using (var image = Image.Load(memoryString))
                    {
                        image.SaveAsJpeg(outputStream);
                        outputStream.Position = 0;
                        var filename = string.Join(".", file.FileName.Split('.').SkipLast(1).Append("jpg"));
                        return new FormFile(outputStream, 0, outputStream.Length, file.Name, $"{filename}")
                        {
                            Headers = new HeaderDictionary
                            {
                                { "Content-Type", ManagedMediaTypes.Jpg },
                                { "Content-Disposition", $"form-data; name=\"{file.Name}\"; filename=\"{filename}\"" },
                            },
                            ContentType = ManagedMediaTypes.Jpg,
                        };
                    }
                }
            }

            return file;
        }

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
