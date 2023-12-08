namespace Husa.Extensions.Downloader.Trestle.Helpers.Parsers
{
    using System.Collections.Generic;
    using System.IO;
    using Husa.Extensions.Common;
    using Husa.Extensions.Downloader.Trestle.Models.Enums;

    public class MultipartImage
    {
        private readonly Dictionary<string, string> headers;
        private readonly MemoryStream image;

        public MultipartImage(Dictionary<string, string> headers, MemoryStream image)
        {
            this.headers = new Dictionary<string, string>(headers);
            this.image = new MemoryStream();
            image.Seek(0, SeekOrigin.Begin);
            image.CopyTo(this.image);
        }

        public Dictionary<string, string> Headers => this.headers;
        public MemoryStream Image => this.image;

        public string GetId() => this.Headers["Content-ID"];
        public int GetOrder() => int.Parse(this.Headers["OrderHint"]);
        public string GetFilename()
        {
            if (this.Headers.TryGetValue("Content-Description", out var content))
            {
                return $"{content}.{this.Headers["Content-Type"].GetEnumFromText<MediaType>()}";
            }

            return $"{this.Headers["OrderHint"]}.{this.Headers["Content-Type"].GetEnumFromText<MediaType>()}";
        }

        public string GetMediaType() => this.Headers["Content-Type"];
    }
}
