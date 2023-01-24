namespace Husa.Extensions.Downloader.Trestle.Models
{
    using System;
    using Husa.Extensions.Downloader.Trestle.Models.Enums;

    public class PropertyUnitTypes
    {
        public bool HumanModifiedYN { get; set; }
        public string ListAgentKey { get; set; }
        public string ListingId { get; set; }
        public string ListingKey { get; set; }
        public int ListingKeyNumeric { get; set; }
        public string ListOfficeKey { get; set; }
        public DateTimeOffset ModificationTimestamp { get; set; }
        public string OriginatingSystemName { get; set; }
        public string OriginatingSystemSubName { get; set; }
        public Permission Permission { get; set; }
        public string PermissionPrivate { get; set; }
        public decimal UnitTypeActualRent { get; set; }
        public string UnitTypeActualRentRange { get; set; }
        public decimal UnitTypeArea { get; set; }
        public int UnitTypeBathsTotal { get; set; }
        public int UnitTypeBedsTotal { get; set; }
        public decimal UnitTypeDeposit { get; set; }
        public string UnitTypeDescription { get; set; }
        public bool UnitTypeFireplaceYN { get; set; }
        public Furnished UnitTypeFurnished { get; set; }
        public bool UnitTypeGarageAttachedYN { get; set; }
        public decimal UnitTypeGarageSpaces { get; set; }
        public string UnitTypeKey { get; set; }
        public int UnitTypeKeyNumeric { get; set; }
        public bool UnitTypeLeasedYN { get; set; }
        public DateTime UnitTypeLeaseExpires { get; set; }
        public bool UnitTypeMonthToMonthYN { get; set; }
        public int UnitTypeNumFullBaths { get; set; }
        public int UnitTypeNumHalfBaths { get; set; }
        public UnitTypeOccupantType UnitTypeOccupantType { get; set; }
        public decimal UnitTypePetDeposit { get; set; }
        public bool UnitTypePetDepositPerPetYN { get; set; }
        public int UnitTypeProForma { get; set; }
        public decimal UnitTypeTotalRent { get; set; }
        public UnitTypeType UnitTypeType { get; set; }
        public string UnitTypeUnitNum { get; set; }
        public int UnitTypeUnitsTotal { get; set; }
    }
}
