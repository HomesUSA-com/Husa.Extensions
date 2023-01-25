namespace Husa.Extensions.Downloader.Trestle.Contracts
{
    using System.Collections.Generic;
    using System.IO;

    public class MultipartImage
    {
        private readonly MemoryStream image;
        private Dictionary<string, string> headers;

        public MultipartImage(Dictionary<string, string> headers, MemoryStream image)
        {
            this.headers = new Dictionary<string, string>(headers);
            this.image = new MemoryStream();
            image.Seek(0, SeekOrigin.Begin);
            image.CopyTo(this.image);
        }

        public Dictionary<string, string> Headers => this.headers;
        public MemoryStream Image => this.image;
    }
}
