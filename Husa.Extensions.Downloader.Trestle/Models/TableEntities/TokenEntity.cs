namespace Husa.Extensions.Downloader.Trestle.Models.TableEntities
{
    using System;
    using Azure;
    using Azure.Data.Tables;

    public class TokenEntity : ITableEntity
    {
        public string AccessToken { get; set; }
        public DateTimeOffset ExpireDate { get; set; }
        public string TokenType { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
