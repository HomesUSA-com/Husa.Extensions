namespace Husa.Extensions.Quickbooks.Models.Invoice.AttachResponse
{
    public class Attachable
    {
        public string Id { get; set; }

        public string SyncToken { get; set; }

        public MetaDataAttach MetaData { get; set; }

        public AttachableRef AttachableRef { get; set; }

        public string FileName { get; set; }

        public string FileAccessUri { get; set; }

        public string TempDownloadUri { get; set; }

        public string Size { get; set; }

        public string ContentType { get; set; }
    }
}
