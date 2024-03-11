namespace Husa.Extensions.Document.Tests.Providers.Models
{
    using Husa.Extensions.Common.Enums;
    using Husa.Extensions.Document.Extensions;
    using Husa.Extensions.Document.ValueObjects;

    public record AddressRecord
    {
        public const string SummarySection = "AddressInfo";
        public string FormalAddress { get; set; }
        public string ReadableCity { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public States State { get; set; }
        public string ZipCode { get; set; }
        public string UnitNumber { get; set; }

        public virtual SummarySection GetSummary<T>(T entity)
            where T : class
            => this.GetSummarySection(entity, sectionName: SummarySection);
    }
}
