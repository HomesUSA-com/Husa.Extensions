namespace Husa.Extensions.Document.Interfaces
{
    using System;
    using Newtonsoft.Json;

    public interface IDocument
    {
        [JsonProperty(PropertyName = "id")]
        Guid Id { get; set; }
        Guid EntityId { get; }
        bool IsDeleted { get; set; }
        int? LegacyId { get; }
        DateTime SysCreatedOn { get; set; }
        void UpdateTrackValues(Guid userId, bool isNewRecord = false);
    }
}
