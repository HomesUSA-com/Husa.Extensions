namespace Husa.Extensions.Downloader.Trestle.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Husa.Extensions.Downloader.Trestle.Helpers.Converters;
    using Husa.Extensions.Downloader.Trestle.Models.Enums;
    using Microsoft.Spatial;

    public class Property
    {
        public decimal? AboveGradeFinishedArea { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AreaSource?>))]
        public AreaSource? AboveGradeFinishedAreaSource { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AreaUnits?>))]
        public AreaUnits? AboveGradeFinishedAreaUnits { get; set; }
        public string AccessCode { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<AccessibilityFeatures>))]
        public IEnumerable<AccessibilityFeatures> AccessibilityFeatures { get; set; }
        public DateTime? ActivationDate { get; set; }
        public string AdditionalParcelsDescription { get; set; }
        public bool? AdditionalParcelsYN { get; set; }
        public string AnchorsCoTenants { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<Appliances>))]
        public IEnumerable<Appliances> Appliances { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<ArchitecturalStyle>))]
        public IEnumerable<ArchitecturalStyle> ArchitecturalStyle { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<AssociationAmenities>))]
        public IEnumerable<AssociationAmenities> AssociationAmenities { get; set; }
        public decimal? AssociationFee { get; set; }
        public decimal? AssociationFee2 { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<FeeFrequency?>))]
        public FeeFrequency? AssociationFee2Frequency { get; set; }
        public decimal? AssociationFee3 { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<FeeFrequency?>))]
        public FeeFrequency? AssociationFee3Frequency { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<FeeFrequency?>))]
        public FeeFrequency? AssociationFeeFrequency { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<AssociationFeeIncludes>))]
        public IEnumerable<AssociationFeeIncludes> AssociationFeeIncludes { get; set; }
        public string AssociationName { get; set; }
        public string AssociationName2 { get; set; }
        public string AssociationPhone { get; set; }
        public string AssociationPhone2 { get; set; }
        public bool? AssociationYN { get; set; }
        public bool? AttachedGarageYN { get; set; }
        public string AttributionContact { get; set; }
        public DateTime? AvailabilityDate { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<Basement>))]
        public IEnumerable<Basement> Basement { get; set; }
        public bool? BasementYN { get; set; }
        public int? BathroomsFull { get; set; }
        public int? BathroomsHalf { get; set; }
        public int? BathroomsOneQuarter { get; set; }
        public int? BathroomsPartial { get; set; }
        public int? BathroomsThreeQuarter { get; set; }
        public int? BathroomsTotalInteger { get; set; }
        public int? BedroomsPossible { get; set; }
        public int? BedroomsTotal { get; set; }
        public decimal? BelowGradeFinishedArea { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AreaSource?>))]
        public AreaSource? BelowGradeFinishedAreaSource { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AreaUnits?>))]
        public AreaUnits? BelowGradeFinishedAreaUnits { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<BodyType?>))]
        public BodyType? BodyType { get; set; }
        public string BuilderModel { get; set; }
        public string BuilderName { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AreaSource?>))]
        public AreaSource? BuildingAreaSource { get; set; }
        public decimal? BuildingAreaTotal { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AreaUnits?>))]
        public AreaUnits? BuildingAreaUnits { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<BuildingFeatures>))]
        public IEnumerable<BuildingFeatures> BuildingFeatures { get; set; }
        public string BuildingName { get; set; }
        public string BusinessName { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<BusinessType?>))]
        public BusinessType? BusinessType { get; set; }
        public string BuyerAgencyCompensation { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<CompensationType>))]
        public IEnumerable<CompensationType> BuyerAgencyCompensationType { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AOR?>))]
        public AOR? BuyerAgentAOR { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<BuyerAgentDesignation?>))]
        public BuyerAgentDesignation? BuyerAgentDesignation { get; set; }
        public string BuyerAgentDirectPhone { get; set; }
        public string BuyerAgentEmail { get; set; }
        public string BuyerAgentFax { get; set; }
        public string BuyerAgentFirstName { get; set; }
        public string BuyerAgentFullName { get; set; }
        public string BuyerAgentHomePhone { get; set; }
        public string BuyerAgentKey { get; set; }
        public int? BuyerAgentKeyNumeric { get; set; }
        public string BuyerAgentLastName { get; set; }
        public string BuyerAgentMiddleName { get; set; }
        public string BuyerAgentMlsId { get; set; }
        public string BuyerAgentMobilePhone { get; set; }
        public string BuyerAgentNamePrefix { get; set; }
        public string BuyerAgentNameSuffix { get; set; }
        public string BuyerAgentOfficePhone { get; set; }
        public string BuyerAgentOfficePhoneExt { get; set; }
        public string BuyerAgentPager { get; set; }
        public string BuyerAgentPreferredPhone { get; set; }
        public string BuyerAgentPreferredPhoneExt { get; set; }
        public string BuyerAgentStateLicense { get; set; }
        public string BuyerAgentTollFreePhone { get; set; }
        public string BuyerAgentURL { get; set; }
        public string BuyerAgentVoiceMail { get; set; }
        public string BuyerAgentVoiceMailExt { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<BuyerFinancing>))]
        public IEnumerable<BuyerFinancing> BuyerFinancing { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AOR?>))]
        public AOR? BuyerOfficeAOR { get; set; }
        public string BuyerOfficeEmail { get; set; }
        public string BuyerOfficeFax { get; set; }
        public string BuyerOfficeKey { get; set; }
        public int? BuyerOfficeKeyNumeric { get; set; }
        public string BuyerOfficeMlsId { get; set; }
        public string BuyerOfficeName { get; set; }
        public string BuyerOfficePhone { get; set; }
        public string BuyerOfficePhoneExt { get; set; }
        public string BuyerOfficeURL { get; set; }
        public string BuyerTeamKey { get; set; }
        public int? BuyerTeamKeyNumeric { get; set; }
        public string BuyerTeamName { get; set; }
        public decimal? CableTvExpense { get; set; }
        public DateTime? CancellationDate { get; set; }
        public decimal? CapRate { get; set; }
        public decimal? CarportSpaces { get; set; }
        public bool? CarportYN { get; set; }
        public string CarrierRoute { get; set; }
        public string City { get; set; }
        public string CityRegion { get; set; }
        public long? CLIP { get; set; }
        public DateTime? CloseDate { get; set; }
        public decimal? ClosePrice { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AOR?>))]
        public AOR? CoBuyerAgentAOR { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<CoBuyerAgentDesignation?>))]
        public CoBuyerAgentDesignation? CoBuyerAgentDesignation { get; set; }
        public string CoBuyerAgentDirectPhone { get; set; }
        public string CoBuyerAgentEmail { get; set; }
        public string CoBuyerAgentFax { get; set; }
        public string CoBuyerAgentFirstName { get; set; }
        public string CoBuyerAgentFullName { get; set; }
        public string CoBuyerAgentHomePhone { get; set; }
        public string CoBuyerAgentKey { get; set; }
        public int? CoBuyerAgentKeyNumeric { get; set; }
        public string CoBuyerAgentLastName { get; set; }
        public string CoBuyerAgentMiddleName { get; set; }
        public string CoBuyerAgentMlsId { get; set; }
        public string CoBuyerAgentMobilePhone { get; set; }
        public string CoBuyerAgentNamePrefix { get; set; }
        public string CoBuyerAgentNameSuffix { get; set; }
        public string CoBuyerAgentOfficePhone { get; set; }
        public string CoBuyerAgentOfficePhoneExt { get; set; }
        public string CoBuyerAgentPager { get; set; }
        public string CoBuyerAgentPreferredPhone { get; set; }
        public string CoBuyerAgentPreferredPhoneExt { get; set; }
        public string CoBuyerAgentStateLicense { get; set; }
        public string CoBuyerAgentTollFreePhone { get; set; }
        public string CoBuyerAgentURL { get; set; }
        public string CoBuyerAgentVoiceMail { get; set; }
        public string CoBuyerAgentVoiceMailExt { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AOR?>))]
        public AOR? CoBuyerOfficeAOR { get; set; }
        public string CoBuyerOfficeEmail { get; set; }
        public string CoBuyerOfficeFax { get; set; }
        public string CoBuyerOfficeKey { get; set; }
        public int? CoBuyerOfficeKeyNumeric { get; set; }
        public string CoBuyerOfficeMlsId { get; set; }
        public string CoBuyerOfficeName { get; set; }
        public string CoBuyerOfficePhone { get; set; }
        public string CoBuyerOfficePhoneExt { get; set; }
        public string CoBuyerOfficeURL { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AOR?>))]
        public AOR? CoListAgentAOR { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<CoListAgentDesignation?>))]
        public CoListAgentDesignation? CoListAgentDesignation { get; set; }
        public string CoListAgentDirectPhone { get; set; }
        public string CoListAgentEmail { get; set; }
        public string CoListAgentFax { get; set; }
        public string CoListAgentFirstName { get; set; }
        public string CoListAgentFullName { get; set; }
        public string CoListAgentHomePhone { get; set; }
        public string CoListAgentKey { get; set; }
        public int? CoListAgentKeyNumeric { get; set; }
        public string CoListAgentLastName { get; set; }
        public string CoListAgentMiddleName { get; set; }
        public string CoListAgentMlsId { get; set; }
        public string CoListAgentMobilePhone { get; set; }
        public string CoListAgentNamePrefix { get; set; }
        public string CoListAgentNameSuffix { get; set; }
        public string CoListAgentOfficePhone { get; set; }
        public string CoListAgentOfficePhoneExt { get; set; }
        public string CoListAgentPager { get; set; }
        public string CoListAgentPreferredPhone { get; set; }
        public string CoListAgentPreferredPhoneExt { get; set; }
        public string CoListAgentStateLicense { get; set; }
        public string CoListAgentTollFreePhone { get; set; }
        public string CoListAgentURL { get; set; }
        public string CoListAgentVoiceMail { get; set; }
        public string CoListAgentVoiceMailExt { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AOR?>))]
        public AOR? CoListOfficeAOR { get; set; }
        public string CoListOfficeEmail { get; set; }
        public string CoListOfficeFax { get; set; }
        public string CoListOfficeKey { get; set; }
        public int? CoListOfficeKeyNumeric { get; set; }
        public string CoListOfficeMlsId { get; set; }
        public string CoListOfficeName { get; set; }
        public string CoListOfficePhone { get; set; }
        public string CoListOfficePhoneExt { get; set; }
        public string CoListOfficeURL { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<CommonInterest?>))]
        public CommonInterest? CommonInterest { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<CommonWalls>))]
        public IEnumerable<CommonWalls> CommonWalls { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<CommunityFeatures>))]
        public IEnumerable<CommunityFeatures> CommunityFeatures { get; set; }
        public string CompensationComments { get; set; }
        public bool? CompSaleYN { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<Concessions?>))]
        public Concessions? Concessions { get; set; }
        public int? ConcessionsAmount { get; set; }
        public string ConcessionsComments { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<ConstructionMaterials>))]
        public IEnumerable<ConstructionMaterials> ConstructionMaterials { get; set; }
        public string ContinentRegion { get; set; }
        public string Contingency { get; set; }
        public DateTime? ContingentDate { get; set; }
        public DateTime? ContractStatusChangeDate { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<Cooling>))]
        public IEnumerable<Cooling> Cooling { get; set; }
        public bool? CoolingYN { get; set; }
        public string CopyrightNotice { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<Country?>))]
        public Country? Country { get; set; }
        public string CountryRegion { get; set; }
        public string CountyOrParish { get; set; }
        public decimal? CoveredSpaces { get; set; }
        public bool? CropsIncludedYN { get; set; }
        public string CrossStreet { get; set; }
        public decimal? CultivatedArea { get; set; }
        public int? CumulativeDaysOnMarket { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<CurrentFinancing>))]
        public IEnumerable<CurrentFinancing> CurrentFinancing { get; set; }
        public decimal? CurrentPrice { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<CurrentUse>))]
        public IEnumerable<CurrentUse> CurrentUse { get; set; }
        public int? DaysOnMarket { get; set; }
        public int? DaysOnMarketReplication { get; set; }
        public DateTime? DaysOnMarketReplicationDate { get; set; }
        public bool? DaysOnMarketReplicationIncreasingYN { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<DevelopmentStatus>))]
        public IEnumerable<DevelopmentStatus> DevelopmentStatus { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<DirectionFaces?>))]
        public DirectionFaces? DirectionFaces { get; set; }
        public string Directions { get; set; }
        public string Disclaimer { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<Disclosures>))]
        public IEnumerable<Disclosures> Disclosures { get; set; }
        public string DistanceToBusComments { get; set; }
        public int? DistanceToBusNumeric { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<LinearUnits?>))]
        public LinearUnits? DistanceToBusUnits { get; set; }
        public string DistanceToElectricComments { get; set; }
        public int? DistanceToElectricNumeric { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<LinearUnits?>))]
        public LinearUnits? DistanceToElectricUnits { get; set; }
        public string DistanceToFreewayComments { get; set; }
        public int? DistanceToFreewayNumeric { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<LinearUnits?>))]
        public LinearUnits? DistanceToFreewayUnits { get; set; }
        public string DistanceToGasComments { get; set; }
        public int? DistanceToGasNumeric { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<LinearUnits?>))]
        public LinearUnits? DistanceToGasUnits { get; set; }
        public string DistanceToPhoneServiceComments { get; set; }
        public int? DistanceToPhoneServiceNumeric { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<LinearUnits?>))]
        public LinearUnits? DistanceToPhoneServiceUnits { get; set; }
        public string DistanceToPlaceofWorshipComments { get; set; }
        public int? DistanceToPlaceofWorshipNumeric { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<LinearUnits?>))]
        public LinearUnits? DistanceToPlaceofWorshipUnits { get; set; }
        public string DistanceToSchoolBusComments { get; set; }
        public int? DistanceToSchoolBusNumeric { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<LinearUnits?>))]
        public LinearUnits? DistanceToSchoolBusUnits { get; set; }
        public string DistanceToSchoolsComments { get; set; }
        public int? DistanceToSchoolsNumeric { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<LinearUnits?>))]
        public LinearUnits? DistanceToSchoolsUnits { get; set; }
        public string DistanceToSewerComments { get; set; }
        public int? DistanceToSewerNumeric { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<LinearUnits?>))]
        public LinearUnits? DistanceToSewerUnits { get; set; }
        public string DistanceToShoppingComments { get; set; }
        public int? DistanceToShoppingNumeric { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<LinearUnits?>))]
        public LinearUnits? DistanceToShoppingUnits { get; set; }
        public string DistanceToStreetComments { get; set; }
        public int? DistanceToStreetNumeric { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<LinearUnits?>))]
        public LinearUnits? DistanceToStreetUnits { get; set; }
        public string DistanceToWaterComments { get; set; }
        public int? DistanceToWaterNumeric { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<LinearUnits?>))]
        public LinearUnits? DistanceToWaterUnits { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<DocumentsAvailable>))]
        public IEnumerable<DocumentsAvailable> DocumentsAvailable { get; set; }
        public DateTimeOffset? DocumentsChangeTimestamp { get; set; }
        public int? DocumentsCount { get; set; }
        public string DOH1 { get; set; }
        public string DOH2 { get; set; }
        public string DOH3 { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<DoorFeatures>))]
        public IEnumerable<DoorFeatures> DoorFeatures { get; set; }
        public bool? DualVariableCompensationYN { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<Electric>))]
        public IEnumerable<Electric> Electric { get; set; }
        public decimal? ElectricExpense { get; set; }
        public bool? ElectricOnPropertyYN { get; set; }
        public string ElementarySchool { get; set; }
        public string ElementarySchoolDistrict { get; set; }
        public int? Elevation { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<LinearUnits?>))]
        public LinearUnits? ElevationUnits { get; set; }
        public int? EntryLevel { get; set; }
        public string EntryLocation { get; set; }
        public string Exclusions { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<ExistingLeaseType?>))]
        public ExistingLeaseType? ExistingLeaseType { get; set; }
        public DateTime? ExpirationDate { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<ExteriorFeatures>))]
        public IEnumerable<ExteriorFeatures> ExteriorFeatures { get; set; }
        public bool? FarmCreditServiceInclYN { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AreaSource?>))]
        public AreaSource? FarmLandAreaSource { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AreaUnits?>))]
        public AreaUnits? FarmLandAreaUnits { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<Fencing>))]
        public IEnumerable<Fencing> Fencing { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<FinancialDataSource?>))]
        public FinancialDataSource? FinancialDataSource { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<FireplaceFeatures>))]
        public IEnumerable<FireplaceFeatures> FireplaceFeatures { get; set; }
        public int? FireplacesTotal { get; set; }
        public bool? FireplaceYN { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<Flooring>))]
        public IEnumerable<Flooring> Flooring { get; set; }
        public decimal? FoundationArea { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<FoundationDetails>))]
        public IEnumerable<FoundationDetails> FoundationDetails { get; set; }
        public string FrontageLength { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<FrontageType?>))]
        public FrontageType? FrontageType { get; set; }
        public decimal? FuelExpense { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<Furnished?>))]
        public Furnished? Furnished { get; set; }
        public decimal? FurnitureReplacementExpense { get; set; }
        public decimal? GarageSpaces { get; set; }
        public bool? GarageYN { get; set; }
        public decimal? GardenerExpense { get; set; }
        public bool? GrazingPermitsBlmYN { get; set; }
        public bool? GrazingPermitsForestServiceYN { get; set; }
        public bool? GrazingPermitsPrivateYN { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<GreenEnergyEfficient>))]
        public IEnumerable<GreenEnergyEfficient> GreenEnergyEfficient { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<GreenEnergyGeneration?>))]
        public GreenEnergyGeneration? GreenEnergyGeneration { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<GreenIndoorAirQuality>))]
        public IEnumerable<GreenIndoorAirQuality> GreenIndoorAirQuality { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<GreenLocation?>))]
        public GreenLocation? GreenLocation { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<GreenSustainability?>))]
        public GreenSustainability? GreenSustainability { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<GreenWaterConservation>))]
        public IEnumerable<GreenWaterConservation> GreenWaterConservation { get; set; }
        public decimal? GrossIncome { get; set; }
        public decimal? GrossScheduledIncome { get; set; }
        public bool? HabitableResidenceYN { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<Heating>))]
        public IEnumerable<Heating> Heating { get; set; }
        public bool? HeatingYN { get; set; }
        public string HighSchool { get; set; }
        public string HighSchoolDistrict { get; set; }
        public bool? HomeWarrantyYN { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<HorseAmenities>))]
        public IEnumerable<HorseAmenities> HorseAmenities { get; set; }
        public bool? HorseYN { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<HoursDaysOfOperation?>))]
        public HoursDaysOfOperation? HoursDaysOfOperation { get; set; }
        public string HoursDaysOfOperationDescription { get; set; }
        public bool? HumanModifiedYN { get; set; }
        public string Inclusions { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<IncomeIncludes>))]
        public IEnumerable<IncomeIncludes> IncomeIncludes { get; set; }
        public decimal? InsuranceExpense { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<InteriorOrRoomFeatures>))]
        public IEnumerable<InteriorOrRoomFeatures> InteriorFeatures { get; set; }
        public bool? InternetAddressDisplayYN { get; set; }
        public bool? InternetAutomatedValuationDisplayYN { get; set; }
        public bool? InternetConsumerCommentYN { get; set; }
        public bool? InternetEntireListingDisplayYN { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<IrrigationSource?>))]
        public IrrigationSource? IrrigationSource { get; set; }
        public decimal? IrrigationWaterRightsAcres { get; set; }
        public bool? IrrigationWaterRightsYN { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<LaborInformation?>))]
        public LaborInformation? LaborInformation { get; set; }
        public decimal? LandLeaseAmount { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<FeeFrequency?>))]
        public FeeFrequency? LandLeaseAmountFrequency { get; set; }
        public DateTime? LandLeaseExpirationDate { get; set; }
        public bool? LandLeaseYN { get; set; }
        public decimal? Latitude { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<LaundryFeatures>))]
        public IEnumerable<LaundryFeatures> LaundryFeatures { get; set; }
        public decimal? LeasableArea { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AreaUnits?>))]
        public AreaUnits? LeasableAreaUnits { get; set; }
        public decimal? LeaseAmount { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<FeeFrequency?>))]
        public FeeFrequency? LeaseAmountFrequency { get; set; }
        public bool? LeaseAssignableYN { get; set; }
        public bool? LeaseConsideredYN { get; set; }
        public DateTime? LeaseExpiration { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<LeaseRenewalCompensation>))]
        public IEnumerable<LeaseRenewalCompensation> LeaseRenewalCompensation { get; set; }
        public bool? LeaseRenewalOptionYN { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<LeaseTerm?>))]
        public LeaseTerm? LeaseTerm { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<Levels?>))]
        public Levels? Levels { get; set; }
        public string License1 { get; set; }
        public string License2 { get; set; }
        public string License3 { get; set; }
        public decimal? LicensesExpense { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AOR?>))]
        public AOR? ListAgentAOR { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<ListAgentDesignation?>))]
        public ListAgentDesignation? ListAgentDesignation { get; set; }
        public string ListAgentDirectPhone { get; set; }
        public string ListAgentEmail { get; set; }
        public string ListAgentFax { get; set; }
        public string ListAgentFirstName { get; set; }
        public string ListAgentFullName { get; set; }
        public string ListAgentHomePhone { get; set; }
        public string ListAgentKey { get; set; }
        public int? ListAgentKeyNumeric { get; set; }
        public string ListAgentLastName { get; set; }
        public string ListAgentMiddleName { get; set; }
        public string ListAgentMlsId { get; set; }
        public string ListAgentMobilePhone { get; set; }
        public string ListAgentNamePrefix { get; set; }
        public string ListAgentNameSuffix { get; set; }
        public string ListAgentOfficePhone { get; set; }
        public string ListAgentOfficePhoneExt { get; set; }
        public string ListAgentPager { get; set; }
        public string ListAgentPreferredPhone { get; set; }
        public string ListAgentPreferredPhoneExt { get; set; }
        public string ListAgentStateLicense { get; set; }
        public string ListAgentTollFreePhone { get; set; }
        public string ListAgentURL { get; set; }
        public string ListAgentVoiceMail { get; set; }
        public string ListAgentVoiceMailExt { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AOR?>))]
        public AOR? ListAOR { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<ListingAgreement?>))]
        public ListingAgreement? ListingAgreement { get; set; }
        public DateTime? ListingContractDate { get; set; }
        public string ListingId { get; set; }
        public string ListingKey { get; set; }
        public int? ListingKeyNumeric { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<ListingService?>))]
        public ListingService? ListingService { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<ListingTerms>))]
        public IEnumerable<ListingTerms> ListingTerms { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AOR?>))]
        public AOR? ListOfficeAOR { get; set; }
        public string ListOfficeEmail { get; set; }
        public string ListOfficeFax { get; set; }
        public string ListOfficeKey { get; set; }
        public int? ListOfficeKeyNumeric { get; set; }
        public string ListOfficeMlsId { get; set; }
        public string ListOfficeName { get; set; }
        public string ListOfficePhone { get; set; }
        public string ListOfficePhoneExt { get; set; }
        public string ListOfficeURL { get; set; }
        public decimal? ListPrice { get; set; }
        public decimal? ListPriceLow { get; set; }
        public string ListTeamKey { get; set; }
        public int? ListTeamKeyNumeric { get; set; }
        public string ListTeamName { get; set; }
        public decimal? LivingArea { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AreaSource?>))]
        public AreaSource? LivingAreaSource { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<AreaUnits?>))]
        public AreaUnits? LivingAreaUnits { get; set; }
        public string LockBoxLocation { get; set; }
        public string LockBoxSerialNumber { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<LockBoxType?>))]
        public LockBoxType? LockBoxType { get; set; }
        public decimal? Longitude { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<LotDimensionsSource?>))]
        public LotDimensionsSource? LotDimensionsSource { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<LotFeatures>))]
        public IEnumerable<LotFeatures> LotFeatures { get; set; }
        public decimal? LotSizeAcres { get; set; }
        public decimal? LotSizeArea { get; set; }
        public string LotSizeDimensions { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<LotSizeSource?>))]
        public LotSizeSource? LotSizeSource { get; set; }
        public decimal? LotSizeSquareFeet { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<LotSizeUnits?>))]
        public LotSizeUnits? LotSizeUnits { get; set; }
        public int? MainLevelBathrooms { get; set; }
        public int? MainLevelBedrooms { get; set; }
        public decimal? MaintenanceExpense { get; set; }
        public DateTimeOffset? MajorChangeTimestamp { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<ChangeType?>))]
        public ChangeType? MajorChangeType { get; set; }
        public string Make { get; set; }
        public decimal? ManagerExpense { get; set; }
        public string MapCoordinate { get; set; }
        public string MapCoordinateSource { get; set; }
        public string MapURL { get; set; }
        public string MiddleOrJuniorSchool { get; set; }
        public string MiddleOrJuniorSchoolDistrict { get; set; }
        public string MLSAreaMajor { get; set; }
        public string MLSAreaMinor { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<MlsStatus?>))]
        public MlsStatus? MlsStatus { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<LinearUnits?>))]
        public LinearUnits? MobileDimUnits { get; set; }
        public bool? MobileHomeRemainsYN { get; set; }
        public int? MobileLength { get; set; }
        public int? MobileWidth { get; set; }
        public string Model { get; set; }
        public DateTimeOffset? ModificationTimestamp { get; set; }
        public decimal? NetOperatingIncome { get; set; }
        public bool? NewConstructionYN { get; set; }
        public decimal? NewTaxesExpense { get; set; }
        public int? NumberOfBuildings { get; set; }
        public int? NumberOfFullTimeEmployees { get; set; }
        public int? NumberOfLots { get; set; }
        public int? NumberOfPads { get; set; }
        public int? NumberOfPartTimeEmployees { get; set; }
        public int? NumberOfSeparateElectricMeters { get; set; }
        public int? NumberOfSeparateGasMeters { get; set; }
        public int? NumberOfSeparateWaterMeters { get; set; }
        public int? NumberOfUnitsInCommunity { get; set; }
        public int? NumberOfUnitsLeased { get; set; }
        public int? NumberOfUnitsMoMo { get; set; }
        public int? NumberOfUnitsTotal { get; set; }
        public int? NumberOfUnitsVacant { get; set; }
        public string OccupantName { get; set; }
        public string OccupantPhone { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<OccupantType?>))]
        public OccupantType? OccupantType { get; set; }
        public DateTime? OffMarketDate { get; set; }
        public DateTimeOffset? OffMarketTimestamp { get; set; }
        public DateTime? OnMarketDate { get; set; }
        public DateTimeOffset? OnMarketTimestamp { get; set; }
        public decimal? OpenParkingSpaces { get; set; }
        public bool? OpenParkingYN { get; set; }
        public decimal? OperatingExpense { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<OperatingExpenseIncludes?>))]
        public OperatingExpenseIncludes? OperatingExpenseIncludes { get; set; }
        public DateTimeOffset? OriginalEntryTimestamp { get; set; }
        public decimal? OriginalListPrice { get; set; }
        public string OriginatingSystemBuyerTeamKey { get; set; }
        public string OriginatingSystemID { get; set; }
        public string OriginatingSystemKey { get; set; }
        public string OriginatingSystemListTeamKey { get; set; }
        public string OriginatingSystemName { get; set; }
        public string OriginatingSystemSubName { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<OtherEquipment>))]
        public IEnumerable<OtherEquipment> OtherEquipment { get; set; }
        public decimal? OtherExpense { get; set; }
        public string OtherParking { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<OtherStructures>))]
        public IEnumerable<OtherStructures> OtherStructures { get; set; }
        public string OwnerName { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<OwnerPays>))]
        public IEnumerable<OwnerPays> OwnerPays { get; set; }
        public string OwnerPhone { get; set; }
        public string Ownership { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<OwnershipType?>))]
        public OwnershipType? OwnershipType { get; set; }
        public string ParcelNumber { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<ParkingFeatures>))]
        public IEnumerable<ParkingFeatures> ParkingFeatures { get; set; }
        public decimal? ParkingTotal { get; set; }
        public string ParkManagerName { get; set; }
        public string ParkManagerPhone { get; set; }
        public string ParkName { get; set; }
        public decimal? PastureArea { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<PatioAndPorchFeatures>))]
        public IEnumerable<PatioAndPorchFeatures> PatioAndPorchFeatures { get; set; }
        public DateTimeOffset? PendingTimestamp { get; set; }

        [JsonConverter(typeof(StringNullableEnumConverter<Permission?>))]
        public Permission? Permission { get; set; }
        public string PermissionPrivate { get; set; }
        public decimal? PestControlExpense { get; set; }
        public decimal? PetDeposit { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<PetsAllowed?>))]
        public PetsAllowed? PetsAllowed { get; set; }
        public DateTimeOffset? PhotosChangeTimestamp { get; set; }
        public int? PhotosCount { get; set; }
        public decimal? PoolExpense { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<PoolFeatures>))]
        public IEnumerable<PoolFeatures> PoolFeatures { get; set; }
        public bool? PoolPrivateYN { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<Possession>))]
        public IEnumerable<Possession> Possession { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<PossibleUse>))]
        public IEnumerable<PossibleUse> PossibleUse { get; set; }
        public string PostalCity { get; set; }
        public string PostalCode { get; set; }
        public string PostalCodePlus4 { get; set; }
        public decimal? PreviousListPrice { get; set; }
        public DateTimeOffset? PriceChangeTimestamp { get; set; }
        public string PrivateOfficeRemarks { get; set; }
        public string PrivateRemarks { get; set; }
        public decimal? ProfessionalManagementExpense { get; set; }
        public bool? PropertyAttachedYN { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<PropertyCondition>))]
        public IEnumerable<PropertyCondition> PropertyCondition { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<PropertySubType>))]
        public IEnumerable<PropertySubType> PropertySubType { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<PropertySubTypeAdditional>))]
        public IEnumerable<PropertySubTypeAdditional> PropertySubTypeAdditional { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<PropertyType>))]
        public IEnumerable<PropertyType> PropertyType { get; set; }
        public string PublicRemarks { get; set; }
        public string PublicSurveyRange { get; set; }
        public string PublicSurveySection { get; set; }
        public string PublicSurveyTownship { get; set; }
        public DateTime? PurchaseContractDate { get; set; }
        public decimal? RangeArea { get; set; }
        public bool? RentControlYN { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<RentIncludes>))]
        public IEnumerable<RentIncludes> RentIncludes { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<RoadFrontageType>))]
        public IEnumerable<RoadFrontageType> RoadFrontageType { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<RoadResponsibility?>))]
        public RoadResponsibility? RoadResponsibility { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<RoadSurfaceType?>))]
        public RoadSurfaceType? RoadSurfaceType { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<Roof>))]
        public IEnumerable<Roof> Roof { get; set; }
        public int? RoomsTotal { get; set; }
        public string RVParkingDimensions { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<SaleOrLeaseIndicator?>))]
        public SaleOrLeaseIndicator? SaleOrLeaseIndicator { get; set; }
        public int? SeatingCapacity { get; set; }
        public decimal? SecurityDeposit { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<SecurityFeatures>))]
        public IEnumerable<SecurityFeatures> SecurityFeatures { get; set; }
        public bool? SeniorCommunityYN { get; set; }
        public string SerialU { get; set; }
        public string SerialX { get; set; }
        public string SerialXX { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<Sewer>))]
        public IEnumerable<Sewer> Sewer { get; set; }
        public int? ShowingAdvanceNotice { get; set; }
        public bool? ShowingAttendedYN { get; set; }
        public string ShowingContactName { get; set; }
        public string ShowingContactPhone { get; set; }
        public string ShowingContactPhoneExt { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<ShowingContactType?>))]
        public ShowingContactType? ShowingContactType { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<ShowingDays?>))]
        public ShowingDays? ShowingDays { get; set; }
        public DateTimeOffset? ShowingEndTime { get; set; }
        public string ShowingInstructions { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<ShowingRequirements>))]
        public IEnumerable<ShowingRequirements> ShowingRequirements { get; set; }
        public DateTimeOffset? ShowingStartTime { get; set; }
        public bool? SignOnPropertyYN { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<Skirt>))]
        public IEnumerable<Skirt> Skirt { get; set; }
        public string SourceSystemID { get; set; }
        public string SourceSystemKey { get; set; }
        public string SourceSystemName { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<SpaFeatures>))]
        public IEnumerable<SpaFeatures> SpaFeatures { get; set; }
        public bool? SpaYN { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<SpecialLicenses>))]
        public IEnumerable<SpecialLicenses> SpecialLicenses { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<SpecialListingConditions>))]
        public IEnumerable<SpecialListingConditions> SpecialListingConditions { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<StandardStatus?>))]
        public StandardStatus? StandardStatus { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<StateOrProvince?>))]
        public StateOrProvince? StateOrProvince { get; set; }
        public string StateRegion { get; set; }
        public DateTimeOffset? StatusChangeTimestamp { get; set; }
        public int? Stories { get; set; }
        public int? StoriesTotal { get; set; }
        public string StreetAdditionalInfo { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<StreetDirection?>))]
        public StreetDirection? StreetDirPrefix { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<StreetDirection?>))]
        public StreetDirection? StreetDirSuffix { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public int? StreetNumberNumeric { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<StreetSuffix?>))]
        public StreetSuffix? StreetSuffix { get; set; }
        public string StreetSuffixModifier { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<StructureType?>))]
        public StructureType? StructureType { get; set; }
        public string SubAgencyCompensation { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<CompensationType>))]
        public IEnumerable<CompensationType> SubAgencyCompensationType { get; set; }
        public string SubdivisionName { get; set; }
        public decimal? SuppliesExpense { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<SyndicateTo>))]
        public IEnumerable<SyndicateTo> SyndicateTo { get; set; }
        public string SyndicationRemarks { get; set; }
        public decimal? TaxAnnualAmount { get; set; }
        public int? TaxAssessedValue { get; set; }
        public string TaxBlock { get; set; }
        public string TaxBookNumber { get; set; }
        public string TaxLegalDescription { get; set; }
        public string TaxLot { get; set; }
        public string TaxMapNumber { get; set; }
        public decimal? TaxOtherAnnualAssessmentAmount { get; set; }
        public string TaxParcelLetter { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<TaxStatusCurrent?>))]
        public TaxStatusCurrent? TaxStatusCurrent { get; set; }
        public string TaxTract { get; set; }
        public int? TaxYear { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<TenantPays?>))]
        public TenantPays? TenantPays { get; set; }
        public string Topography { get; set; }
        public decimal? TotalActualRent { get; set; }
        public int? TotalDocumentsCount { get; set; }
        public int? TotalPhotosCount { get; set; }
        public string Township { get; set; }
        public string TransactionBrokerCompensation { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<CompensationType>))]
        public IEnumerable<CompensationType> TransactionBrokerCompensationType { get; set; }
        public decimal? TrashExpense { get; set; }
        public string UnitNumber { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<UnitsFurnished?>))]
        public UnitsFurnished? UnitsFurnished { get; set; }
        public string UniversalPropertyId { get; set; }
        public string UniversalPropertySubId { get; set; }
        public string UnparsedAddress { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<Utilities>))]
        public IEnumerable<Utilities> Utilities { get; set; }
        public decimal? UtilitiesExpense { get; set; }
        public int? VacancyAllowance { get; set; }
        public decimal? VacancyAllowanceRate { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<Vegetation>))]
        public IEnumerable<Vegetation> Vegetation { get; set; }
        public DateTimeOffset? VideosChangeTimestamp { get; set; }
        public int? VideosCount { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<View>))]
        public IEnumerable<View> View { get; set; }
        public bool? ViewYN { get; set; }
        public string VirtualTourURLBranded { get; set; }
        public string VirtualTourURLBranded2 { get; set; }
        public string VirtualTourURLBranded3 { get; set; }
        public string VirtualTourURLUnbranded { get; set; }
        public string VirtualTourURLUnbranded2 { get; set; }
        public string VirtualTourURLUnbranded3 { get; set; }
        public int? WalkScore { get; set; }
        public string WaterBodyName { get; set; }

        [JsonConverter(typeof(StringListEnumConverter<WaterfrontFeatures>))]
        public IEnumerable<WaterfrontFeatures> WaterfrontFeatures { get; set; }
        public bool? WaterfrontYN { get; set; }
        public decimal? WaterSewerExpense { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<WaterSource>))]
        public IEnumerable<WaterSource> WaterSource { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<WindowFeatures>))]
        public IEnumerable<WindowFeatures> WindowFeatures { get; set; }
        public DateTime? WithdrawnDate { get; set; }
        public decimal? WoodedArea { get; set; }
        public decimal? WorkmansCompensationExpense { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<GeocodeSource?>))]
        public GeocodeSource? X_GeocodeSource { get; set; }
        [JsonConverter(typeof(StringNullableEnumConverter<X_LandTenure?>))]
        public X_LandTenure? X_LandTenure { get; set; }
        public string X_LivingAreaRange { get; set; }
        public GeographyPoint X_Location { get; set; }
        public int? YearBuilt { get; set; }
        public string YearBuiltDetails { get; set; }
        public int? YearBuiltEffective { get; set; }
        [JsonConverter(typeof(StringListEnumConverter<YearBuiltSource>))]
        public IEnumerable<YearBuiltSource> YearBuiltSource { get; set; }
        public int? YearEstablished { get; set; }
        public int? YearsCurrentOwner { get; set; }
        public string Zoning { get; set; }
        public string ZoningDescription { get; set; }
        public IEnumerable<OpenHouse> OpenHouse { get; set; }
        public IEnumerable<PropertyRooms> Rooms { get; set; }
    }
}
