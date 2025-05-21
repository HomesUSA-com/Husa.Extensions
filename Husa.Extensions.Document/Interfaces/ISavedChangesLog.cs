namespace Husa.Extensions.Document.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Husa.Extensions.Document.Models;

    public interface ISavedChangesLog
    {
         Guid Id { get; set; }
         Guid EntityId { get; set; }
         Guid UserId { get; set; }
         string UserName { get; set; }
         DateTime SavedAt { get; set; }
         Dictionary<string, ChangedField> Fields { get; set; }
    }
}
