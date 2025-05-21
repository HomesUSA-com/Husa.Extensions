namespace Husa.Extensions.Document.Models
{
    using System;
    using System.Collections.Generic;
    using Husa.Extensions.Document.Interfaces;

    public class SavedChangesLogQueryResult : ISavedChangesLog
    {
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime SavedAt { get; set; }
        public Dictionary<string, ChangedField> Fields { get; set; }
    }
}
