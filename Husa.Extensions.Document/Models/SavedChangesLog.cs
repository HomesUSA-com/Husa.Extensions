namespace Husa.Extensions.Document.Models
{
    using System;
    using System.Collections.Generic;
    using Husa.Extensions.Document.ValueObjects;
    using Newtonsoft.Json;

    public class SavedChangesLog
    {
        public SavedChangesLog()
        {
            this.SavedAt = DateTime.UtcNow;
            this.Id = Guid.NewGuid();
            this.Fields = new HashSet<SummaryField>();
            this.IsUndone = false;
        }

        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime SavedAt { get; set; }
        public bool IsUndone { get; set; }
        public DateTime? UndoneAt { get; set; }
        public IEnumerable<SummaryField> Fields { get; set; }

        public void Undo()
        {
            this.IsUndone = true;
            this.UndoneAt = DateTime.UtcNow;
        }
    }
}
