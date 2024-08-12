namespace Husa.Extensions.OpenAI.Tests;

using System;
using Husa.Extensions.OpenAI.Models;
using Husa.Extensions.OpenAI.Options;

public class PropertyDetailRequestTests
{
    [Fact]
    public void ToString_ShouldIncludeAllProperties()
    {
        // Arrange
        var completionDate = new DateTime(year: 2025, month: 06, day: 21, hour: 0, minute: 0, second: 0, kind: DateTimeKind.Utc);
        var options = new OpenAIOptions();
        var request = new PropertyDetailRequest
        {
            PropertyType = "Residential",
            Subdivision = "Golden Estates",
            HasPool = true,
            TotalPrimaryBedrooms = 1,
            TotalSecondaryBedrooms = 3,
            TotalFullBathrooms = 4,
            TotalHalfBathrooms = 1,
            TotalSquareFootage = 1955,
            TotalLivingSquareFootage = 1655,
            LotSizeAcres = 0.9M,
            YearBuilt = 2020,
            PropertySubType = "Single Family",
            SchoolDistrict = "Alpine ISD",
            ElementarySchool = "Alpine Elementary",
            MiddleSchool = "Alpine",
            HighSchool = "Alpine High",
            GarageSpaces = 2,
            CarportSpaces = 2,
            TotalStories = 2,
            EstimatedCompletionDate = completionDate,
            InteriorFeatures = ["Fireplace", "Hardwood Floors"],
            ExteriorFeatures = ["Patio", "Fence"],
            ParkingFeatures = ["Open", "Tandem"],
            HeatingSystemFeatures = ["Central"],
            CoolingSystemFeatures = ["One Central"],
            LotDescription = ["Cul-de-Sac", "On Golf Course"],
        };

        request.ConfigureUserPrompt(options.UserPrompt, options.MaxReplyCharacters);

        // Act
        var result = request.ToString();

        // Assert
        Assert.Contains("- Property Type: Residential", result);
        Assert.Contains("- Total Bedrooms: 4", result);
        Assert.Contains("- Total Secondary Bedrooms: 3", result);
        Assert.Contains("- Total Primary Bedrooms: 1", result);
        Assert.Contains("- Total Bathrooms: 5", result);
        Assert.Contains("- Total Full Bathrooms: 4", result);
        Assert.Contains("- Total Half Bathrooms: 1", result);
        Assert.Contains("- Total Square Footage: 1955", result);
        Assert.Contains("- Total Living Square Footage: 1655", result);
        Assert.Contains("- Lot Size (Acres): 0.9", result);
        Assert.Contains("- Year Built: 2020", result);
        Assert.Contains("- Property Sub-Type: Single Family", result);
        Assert.Contains("- School District: Alpine ISD", result);
        Assert.Contains("- Elementary School: Alpine Elementary", result);
        Assert.Contains("- Middle School: Alpine", result);
        Assert.Contains("- High School: Alpine High", result);
        Assert.Contains("- Subdivision: Golden Estates", result);
        Assert.Contains("- Estimated Completion Date: 6/21/2025 12:00:00 AM", result);
        Assert.Contains("- Has Pool: True", result);
        Assert.Contains("- Total Stories: 2", result);
        Assert.Contains("- Garage Spaces: 2", result);
        Assert.Contains("- Carport Spaces: 2", result);
        Assert.Contains("- Interior Features: Fireplace, Hardwood Floors", result);
        Assert.Contains("- Exterior Features: Patio, Fence", result);
        Assert.Contains("- Heating System Features: Central", result);
        Assert.Contains("- Cooling System Features: One Central", result);
        Assert.Contains("- Parking Features: Open, Tandem", result);
        Assert.Contains("- Lot Description: Cul-de-Sac, On Golf Course", result);
    }

    [Fact]
    public void ToString_ShouldHandleNullPropertiesGracefully()
    {
        // Arrange
        var request = new PropertyDetailRequest();

        // Act
        var result = request.ToString();

        // Assert
        Assert.Equal(PropertyDetailRequest.DefaultMessage, result);
    }
}
