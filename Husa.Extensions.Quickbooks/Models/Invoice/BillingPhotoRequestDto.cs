namespace Husa.Extensions.Quickbooks.Models.Invoice
{
    using System;
    using Husa.Extensions.Common.Enums;

    public class BillingPhotoRequestDto
    {
        public Guid PhotoRequestId { get; set; }
        public MarketCode Market { get; set; }
        public Guid CompanyId { get; set; }
        public string StreetNum { get; set; }
        public string StreetName { get; set; }
        public string MlsNumber { get; set; }
        public string Subdivision { get; set; }
        public DateTime? ListDate { get; set; }
        public string MarketStatus { get; set; }
        public string PublishType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
