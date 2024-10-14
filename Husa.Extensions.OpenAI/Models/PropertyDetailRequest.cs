namespace Husa.Extensions.OpenAI.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Husa.Extensions.OpenAI.Helpers;

public class PropertyDetailRequest
{
    public const string DefaultMessage = "I'm sorry, please provide me with some details to work on the description.";

    private string userPrompt;
    private int maxReplyCharacters;

    [Description("Property Type")]
    public string PropertyType { get; set; }

    [Description("Total Bedrooms")]
    public int? TotalBedrooms
    {
        get
        {
            if (!this.TotalPrimaryBedrooms.HasValue && !this.TotalSecondaryBedrooms.HasValue)
            {
                return null;
            }

            var primaryBeds = this.TotalPrimaryBedrooms ?? 0;
            var secondaryBeds = this.TotalSecondaryBedrooms ?? 0;

            return primaryBeds + secondaryBeds;
        }
    }

    [Description("Total Secondary Bedrooms")]
    public int? TotalSecondaryBedrooms { get; set; }

    [Description("Total Primary Bedrooms")]
    public int? TotalPrimaryBedrooms { get; set; }

    [Description("Total Bathrooms")]
    public int? TotalBathrooms
    {
        get
        {
            if (!this.TotalFullBathrooms.HasValue && !this.TotalHalfBathrooms.HasValue)
            {
                return null;
            }

            var fullBaths = this.TotalFullBathrooms ?? 0;
            var halfBaths = this.TotalHalfBathrooms ?? 0;
            return fullBaths + halfBaths;
        }
    }

    [Description("Total Full Bathrooms")]
    public int? TotalFullBathrooms { get; set; }

    [Description("Total Half Bathrooms")]
    public int? TotalHalfBathrooms { get; set; }

    [Description("Total Square Footage")]
    public int? TotalSquareFootage { get; set; }

    [Description("Total Living Square Footage")]
    public int? TotalLivingSquareFootage { get; set; }

    [Description("Lot Size (Acres)")]
    public decimal? LotSizeAcres { get; set; }

    [Description("Year Built")]
    public int? YearBuilt { get; set; }

    [Description("Property Sub-Type")]
    public string PropertySubType { get; set; }

    [Description("School District")]
    public string SchoolDistrict { get; set; }

    [Description("Elementary School")]
    public string ElementarySchool { get; set; }

    [Description("Middle School")]
    public string MiddleSchool { get; set; }

    [Description("High School")]
    public string HighSchool { get; set; }

    [Description("Subdivision")]
    public string Subdivision { get; set; }

    [Description("Construction Stage")]
    public string ConstructionStage { get; set; }

    [Description("Estimated Completion Date")]
    public DateTime? EstimatedCompletionDate { get; set; }

    [Description("Front Face Orientation")]
    public string FrontFaceOrientation { get; set; }

    [Description("Has Water Front")]
    public bool? HasWaterFront { get; set; }

    [Description("Has Water Access")]
    public bool? HasWaterAccess { get; set; }

    [Description("Is Gated Community")]
    public bool? IsGatedCommunity { get; set; }

    [Description("Has Sprinkler System")]
    public bool? HasSprinklerSystem { get; set; }

    [Description("Has Pool")]
    public bool? HasPool { get; set; }

    [Description("Total Dining Areas")]
    public int? TotalDiningAreas { get; set; }

    [Description("Total Living Areas")]
    public int? TotalLivingAreas { get; set; }

    [Description("Total Fireplaces")]
    public int? TotalFireplaces { get; set; }

    [Description("Total Stories")]
    public int? TotalStories { get; set; }

    [Description("Garage Spaces")]
    public int? GarageSpaces { get; set; }

    [Description("Carport Spaces")]
    public int? CarportSpaces { get; set; }

    [Description("Interior Features")]
    public IEnumerable<string> InteriorFeatures { get; set; } = [];

    [Description("Exterior Features")]
    public IEnumerable<string> ExteriorFeatures { get; set; } = [];

    [Description("Heating System Features")]
    public IEnumerable<string> HeatingSystemFeatures { get; set; } = [];

    [Description("Cooling System Features")]
    public IEnumerable<string> CoolingSystemFeatures { get; set; } = [];

    [Description("Parking Features")]
    public IEnumerable<string> ParkingFeatures { get; set; } = [];

    [Description("Lot Description")]
    public IEnumerable<string> LotDescription { get; set; } = [];

    [Description("Housing Style Description")]
    public IEnumerable<string> HousingStyleDescription { get; set; } = [];

    [Description("Master Bedroom Description")]
    public IEnumerable<string> MasterBedroomDescription { get; set; } = [];

    [Description("Kitchen Description")]
    public IEnumerable<string> KitchenDescription { get; set; } = [];

    [Description("Appliances Equipment Description")]
    public IEnumerable<string> AppliancesEquipmentDescription { get; set; } = [];

    [Description("Laundry Area Description")]
    public IEnumerable<string> LaundryAreaDescription { get; set; } = [];

    [Description("Fireplace Description")]
    public IEnumerable<string> FireplaceDescription { get; set; } = [];

    [Description("Floors Description")]
    public IEnumerable<string> FloorsDescription { get; set; } = [];

    [Description("Foundation Description")]
    public IEnumerable<string> FoundationDescription { get; set; } = [];

    [Description("Roof Description")]
    public IEnumerable<string> RoofDescription { get; set; } = [];

    [Description("Pool Description")]
    public IEnumerable<string> PoolDescription { get; set; } = [];

    [Description("Energy Features Description")]
    public IEnumerable<string> EnergyFeatures { get; set; } = [];

    [Description("Water Sewer Description")]
    public IEnumerable<string> WaterSewerDescription { get; set; } = [];

    public static implicit operator string(PropertyDetailRequest model) => model != null ? model.ToString() : string.Empty;

    public void ConfigureUserPrompt(string prompt, int maxReplyCharacters)
    {
        this.userPrompt = prompt;
        this.maxReplyCharacters = maxReplyCharacters;
    }

    public override string ToString()
    {
        if (string.IsNullOrEmpty(this.userPrompt))
        {
            return DefaultMessage;
        }

        var prompt = new StringBuilder($"{this.userPrompt}:");
        prompt.AppendLine();

        foreach (var property in this.GetType().GetProperties())
        {
            var propertyValue = property.GetValue(this);
            prompt.AppendLine(obj: this, value: propertyValue, fieldName: property.Name);
        }

        prompt.AppendLine($"To ensure the description is valid in the MLS, it needs to be shorter than {this.maxReplyCharacters} characters long.");

        return prompt.ToString();
    }
}
