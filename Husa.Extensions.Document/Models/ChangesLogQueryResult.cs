namespace Husa.Extensions.Document.Models
{
    using System;

    public class ChangesLogQueryResult
    {
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime SavedAt { get; set; }
    }
}
