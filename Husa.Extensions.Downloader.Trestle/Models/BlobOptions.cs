namespace Husa.Extensions.Downloader.Trestle.Models
{
    public class BlobOptions
    {
        public const string Section = "BlobConfiguration";
        public string ConnectionString { get; set; }
        public string TableName { get; set; }
        public string PartitionKey { get; set; }
    }
}
