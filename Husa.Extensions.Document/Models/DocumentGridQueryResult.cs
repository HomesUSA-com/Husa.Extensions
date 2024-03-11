namespace Husa.Extensions.Document.Models
{
    using System.Collections.Generic;

    public class DocumentGridQueryResult<T>
        where T : class
    {
        public DocumentGridQueryResult(IEnumerable<T> data, string continuationToken)
        {
            this.Data = data;
            this.ContinuationToken = continuationToken;
        }

        public int Total { get; set; }

        public IEnumerable<T> Data { get; set; }

        public string ContinuationToken { get; set; }
    }
}
