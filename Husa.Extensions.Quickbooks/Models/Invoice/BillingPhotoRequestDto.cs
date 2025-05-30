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
        public string BuilderName { get; set; }
        public bool SecondRequest { get; set; }
        public DateTime CompletionDate { get; set; }
        public string Status { get; set; }
        public bool StillsHome { get; set; }
        public bool StillsExteriorOnly { get; set; }
        public bool Twilight { get; set; }
        public bool VirtualTour { get; set; }
        public bool VirtualStagingOneImage { get; set; }
        public bool VirtualStagingThreeImage { get; set; }
        public bool VirtualStagingSixImage { get; set; }
        public bool CommunityOptionOne { get; set; }
        public bool CommunityOptionTwo { get; set; }
        public bool CommunityOptionThree { get; set; }
        public bool TripCharge { get; set; }
        public bool PhotoshopEditing { get; set; }
        public int Order { get; set; }
        public string Type { get; set; }
        public string PlanName { get; set; }
        public string ModelAddress { get; set; }
        public string UnitNumber { get; set; }
        public int? NumberOfPhotosEdited { get; set; }
    }
}
