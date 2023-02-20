namespace Husa.Extensions.Common.Tests
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using Xunit;

    internal enum GarageDescription
    {
        [EnumMember(Value = "NONE")]
        [Description("None/Not Applicable")]
        NotApplicable,
        [EnumMember(Value = "1GAR")]
        [Description("One Car Garage")]
        OneCarGarage,
        [EnumMember(Value = "2GAR")]
        [Description("Two Car Garage")]
        TwoCarGarage,
        [EnumMember(Value = "3GAR")]
        [Description("Three Car Garage")]
        ThreeCarGarage,
        [EnumMember(Value = "4+GAR")]
        [Description("Four or More Car Garage")]
        FourPlusCarGarage,
        [EnumMember(Value = "ATT")]
        [Description("Attached")]
        Attached,
        [EnumMember(Value = "DTCHD")]
        [Description("Detached")]
        Detached,
        [EnumMember(Value = "OVRSZ")]
        [Description("Oversized")]
        Oversized,
        [EnumMember(Value = "REAR")]
        [Description("Rear Entry")]
        RearEntry,
        [EnumMember(Value = "SIDE")]
        [Description("Side Entry")]
        SideEntry,
        [EnumMember(Value = "TANDEM")]
        [Description("Tandem")]
        Tandem,
    }

    public class EnumSerializerTest
    {
        private const string SerializedDbString = "1GAR,2GAR,3GAR,4+GAR,ATT,DTCHD,NONE,OVRSZ,REAR,SIDE,TANDEM";
        private readonly IEnumerable<GarageDescription> garageFeatures = new[]
        {
            GarageDescription.OneCarGarage,
            GarageDescription.TwoCarGarage,
            GarageDescription.ThreeCarGarage,
            GarageDescription.FourPlusCarGarage,
            GarageDescription.Attached,
            GarageDescription.Detached,
            GarageDescription.NotApplicable,
            GarageDescription.Oversized,
            GarageDescription.RearEntry,
            GarageDescription.SideEntry,
            GarageDescription.Tandem,
        };

        [Fact]
        internal void TestEnumSerializationCorrectly()
        {
            // Arrange
            // Act
            var serializedGarageFeatures = this.garageFeatures.ToStringFromEnumMembers();

            // Assert
            Assert.Equal(SerializedDbString, serializedGarageFeatures);
        }

        [Fact]
        internal void TestEnumDeserializationCorrectly()
        {
            // Arrange
            // Act
            var serializedGarageFeatures = SerializedDbString.CsvToEnum<GarageDescription>();

            // Assert
            Assert.All(this.garageFeatures, garageFeature =>
            {
                Assert.Contains(garageFeature, serializedGarageFeatures);
            });
        }

        [Theory]
        [InlineData("NONE", GarageDescription.NotApplicable)]
        [InlineData("None/Not Applicable", GarageDescription.NotApplicable)]
        [InlineData("none/not applicable", GarageDescription.NotApplicable)]
        [InlineData("NotApplicable", GarageDescription.NotApplicable)]
        [InlineData("notapplicable", GarageDescription.NotApplicable)]
        [InlineData("4+GAR", GarageDescription.FourPlusCarGarage)]
        [InlineData("Four or More Car Garage", GarageDescription.FourPlusCarGarage)]
        [InlineData("four or more car garage", GarageDescription.FourPlusCarGarage)]
        [InlineData("FourPlusCarGarage", GarageDescription.FourPlusCarGarage)]
        [InlineData("fourpluscargarage", GarageDescription.FourPlusCarGarage)]
        [InlineData("3GAR", GarageDescription.ThreeCarGarage)]
        [InlineData("Three Car Garage", GarageDescription.ThreeCarGarage)]
        [InlineData("three car garage", GarageDescription.ThreeCarGarage)]
        [InlineData("ThreeCarGarage", GarageDescription.ThreeCarGarage)]
        [InlineData("threecargarage", GarageDescription.ThreeCarGarage)]
        internal void FindEnumFromTextCorrectly(string city, GarageDescription expectedGarageDescription)
        {
            // Act
            var garageDescriptionEnum = city.GetEnumFromText<GarageDescription>();

            // Assert
            Assert.Equal(expectedGarageDescription, garageDescriptionEnum);
        }
    }
}
