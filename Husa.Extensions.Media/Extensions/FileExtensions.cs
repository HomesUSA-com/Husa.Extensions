namespace Husa.Extensions.Media.Extensions
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Processing;

    public static class FileExtensions
    {
        public static Stream ResizeAndOpenStream(this IFormFile file, int width, int height)
        {
            var image = Image.Load(file.OpenReadStream());
            image.Resize(width, height, false);
            var memoryStream = new MemoryStream();
            image.Save(memoryStream, image.Metadata.DecodedImageFormat);
            return memoryStream;
        }

        public static Dictionary<string, string> Metadata(this IFormFile file, Dictionary<string, string> extraInfo = null)
        {
            var metadata = new Dictionary<string, string>
            {
                { "FileName", file.FileName ?? string.Empty },
                { "FileContentType", file.ContentType },
            };
            extraInfo?.ToList().ForEach(item => metadata.Add(item.Key, item.Value));
            return metadata;
        }

        public static void Resize(this Image imgPhoto, int width, int height, bool maintainRatio)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;

            int destWidth = width;
            int destHeight = height;

            if (maintainRatio)
            {
                float ratioX = width / (float)sourceWidth;
                float ratioY = height / (float)sourceHeight;
                float ratio = (ratioY < ratioX) ? ratioY : ratioX;

                destWidth = (int)(sourceWidth * ratio);
                destHeight = (int)(sourceHeight * ratio);
            }

            imgPhoto.Mutate(x => x.Resize(destWidth, destHeight));
        }
    }
}
