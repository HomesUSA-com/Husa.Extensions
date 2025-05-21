namespace Husa.Extensions.Document.Models
{
    using System;
    using System.Collections.Generic;
    using Husa.Extensions.Document.Interfaces;
    using Newtonsoft.Json;

    public class SavedChangesLog : ISavedChangesLog
    {
        public SavedChangesLog()
        {
            this.SavedAt = DateTime.UtcNow;
            this.Id = Guid.NewGuid();
            this.Fields = new Dictionary<string, ChangedField>();
        }

        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime SavedAt { get; set; }
        public Dictionary<string, ChangedField> Fields { get; set; }
    }
}
