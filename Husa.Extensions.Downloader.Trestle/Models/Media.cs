namespace Husa.Extensions.Downloader.Trestle.Models
{
    using System;
    using System.IO;
    using Husa.Extensions.Downloader.Trestle.Models.Enums;

    public class Media
    {
        public string ChangedByMemberID { get; set; }
        public string ChangedByMemberKey { get; set; }
        public int ChangedByMemberKeyNumeric { get; set; }
        public ClassName ClassName { get; set; }
        public bool HumanModifiedYN { get; set; }
        public int ImageHeight { get; set; }
        public ImageOf ImageOf { get; set; }
        public ImageSizeDescription ImageSizeDescription { get; set; }
        public int ImageWidth { get; set; }
        public string ListAgentKey { get; set; }
        public string ListOfficeKey { get; set; }
        public string ListOfficeMlsId { get; set; }
        public string LongDescription { get; set; }
        public MediaCategory MediaCategory { get; set; }
        public MediaClassification MediaClassification { get; set; }
        public string MediaHTML { get; set; }
        public string MediaKey { get; set; }
        public int MediaKeyNumeric { get; set; }
        public DateTimeOffset MediaModificationTimestamp { get; set; }
        public string MediaObjectID { get; set; }
        public MediaStatus MediaStatus { get; set; }
        public MediaType MediaType { get; set; }
        public string MediaURL { get; set; }
        public DateTimeOffset ModificationTimestamp { get; set; }
        public int Order { get; set; }
        public string OriginatingSystemID { get; set; }
        public string OriginatingSystemMediaKey { get; set; }
        public string OriginatingSystemName { get; set; }
        public string OriginatingSystemSubName { get; set; }
        public Permission Permission { get; set; }
        public string PermissionPrivate { get; set; }
        public bool PreferredPhotoYN { get; set; }
        public ResourceName ResourceName { get; set; }
        public string ResourceRecordID { get; set; }
        public string ResourceRecordKey { get; set; }
        public int ResourceRecordKeyNumeric { get; set; }
        public string ShortDescription { get; set; }
        public string SourceSystemID { get; set; }
        public string SourceSystemMediaKey { get; set; }
        public string SourceSystemName { get; set; }
        public bool UpstreamYN { get; set; }
        public Stream X_MediaStream { get; set; }
    }
}
