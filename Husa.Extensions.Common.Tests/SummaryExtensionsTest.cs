namespace Husa.Extensions.Common.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.Serialization;
    using Husa.Extensions.Common.ValueObjects;
    using Xunit;

    internal enum DocumentsAvailableDescription
    {
        [EnumMember(Value = "BLDGPLANS")]
        [Description("Building Plans")]
        BuildingPlans,
        [EnumMember(Value = "MUDDIST")]
        [Description("MUD/Water District")]
        MUDWaterDistrict,
        [EnumMember(Value = "PLANSS")]
        [Description("Plans and Specs")]
        PlansAndSpecs,
        [EnumMember(Value = "PLAT")]
        [Description("Plat")]
        Plat,
        [EnumMember(Value = "SBDPL")]
        [Description("Subdivision Plat")]
        SubdivisionPlat,
        [EnumMember(Value = "SBDRE")]
        [Description("Subdivision Restrictions")]
        SubdivisionRestrictions,
        [EnumMember(Value = "SURVE")]
        [Description("Survey")]
        Survey,
        [EnumMember(Value = "NONE")]
        [Description("None")]
        None,
        [EnumMember(Value = "OTHSE")]
        [Description("Other-See Remarks")]
        OtherSeeRemarks,
    }

    internal enum MarketStatuses
    {
        Active,
        ActiveUnderContract,
        Canceled,
        Closed,
        ComingSoon,
        CompSold,
        Contingent,
        Delete,
        Expired,
        Hold,
        Incomplete,
        Leased,
        Pending,
        Terminated,
        Withdrawn,
    }

    internal enum OpenHouseType
    {
        [EnumMember(Value = "Monday")]
        Monday = 0,
        [EnumMember(Value = "Tuesday")]
        Tuesday = 1,
        [EnumMember(Value = "Wednesday")]
        Wednesday = 2,
        [EnumMember(Value = "Thursday")]
        Thursday = 3,
        [EnumMember(Value = "Friday")]
        Friday = 4,
        [EnumMember(Value = "Saturday")]
        Saturday = 5,
        [EnumMember(Value = "Sunday")]
        Sunday = 6,
    }

    public class SummaryExtensionsTest
    {
        [Fact]
        public void GetInnerSummaryGetSummarySuccess()
        {
            // Arrange
            var myBool = true;
            var mySecondBool = false;
            var localProperty = new LocalPropertyInfo(myBool);
            var updatedLocalProperty = new LocalPropertyInfo(mySecondBool);

            // Act
            var result = SummaryExtensions.GetInnerSummary(updatedLocalProperty, localProperty);

            // Assert
            Assert.NotNull(result);
            var summaryFields = Assert.IsAssignableFrom<IEnumerable<SummaryField>>(result);
            Assert.Equal(2, summaryFields.ToList().Count);
        }

        [Fact]
        public void GetInnerSummaryFilerFieldsAddedSuccess()
        {
            // Arrange
            var myBool = true;
            var mySecondBool = false;
            string[] filter = { "MyString" };
            var localProperty = new LocalPropertyInfo(myBool);
            var updatedLocalProperty = new LocalPropertyInfo(mySecondBool);

            // Act
            var result = SummaryExtensions.GetInnerSummary(updatedLocalProperty, localProperty, filter);

            // Assert
            Assert.NotNull(result);
            var summaryFields = Assert.IsAssignableFrom<IEnumerable<SummaryField>>(result);
            Assert.Equal(2, summaryFields.ToList().Count);
        }

        [Fact]
        public void GetFieldSummaryBoolObjectSameObjectsSuccess()
        {
            // Arrange
            var myBool = true;
            var localProperty = new LocalPropertyInfo(myBool);
            var updatedLocalProperty = new LocalPropertyInfo(myBool);

            // Act
            var result = SummaryExtensions.GetFieldSummary(localProperty, updatedLocalProperty, isInnerSummary: true);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetFieldSummaryStringObjectSameObjectsSuccess()
        {
            // Arrange
            var testString = "string";
            var localProperty = new LocalPropertyInfo(testString);
            var updatedLocalProperty = new LocalPropertyInfo(testString);

            // Act
            var result = SummaryExtensions.GetFieldSummary(localProperty, updatedLocalProperty, isInnerSummary: true);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetFieldSummaryStringObjectDifferentObjectsSuccess()
        {
            // Arrange
            var testString = "string";
            var updatedTestString = "string2";
            var localProperty = new LocalPropertyInfo(testString);
            var updatedLocalProperty = new LocalPropertyInfo(updatedTestString);

            // Act
            var result = SummaryExtensions.GetFieldSummary(localProperty, updatedLocalProperty, isInnerSummary: true).ToList();

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetFieldSummaryEqualObjectsDifferentOrderSuccess()
        {
            // Arrange
            var documents1 = new HashSet<DocumentsAvailableDescription>
            {
                DocumentsAvailableDescription.BuildingPlans,
                DocumentsAvailableDescription.PlansAndSpecs,
            };
            var localProperty = new LocalPropertyInfo(documents1);

            var updatedDocuments = new HashSet<DocumentsAvailableDescription>
            {
                DocumentsAvailableDescription.PlansAndSpecs,
                DocumentsAvailableDescription.BuildingPlans,
            };
            var updatedLocalProperty = new LocalPropertyInfo(updatedDocuments);

            // Act
            var result = SummaryExtensions.GetFieldSummary(localProperty, updatedLocalProperty, isInnerSummary: true).ToList();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetFieldSummaryEqualObjectsEqualOrderSuccess()
        {
            // Arrange
            var documents = new HashSet<DocumentsAvailableDescription>
            {
                DocumentsAvailableDescription.PlansAndSpecs,
                DocumentsAvailableDescription.BuildingPlans,
            };
            var localProperty = new LocalPropertyInfo(documents);

            var updatedDocuments = new HashSet<DocumentsAvailableDescription>
            {
                DocumentsAvailableDescription.PlansAndSpecs,
                DocumentsAvailableDescription.BuildingPlans,
            };
            var updatedLocalProperty = new LocalPropertyInfo(updatedDocuments);

            // Act
            var result = SummaryExtensions.GetFieldSummary(localProperty, updatedLocalProperty, isInnerSummary: true).ToList();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetFieldSummaryDifferentCollectionsObjectsSuccess()
        {
            // Arrange
            var documents = new HashSet<DocumentsAvailableDescription>
            {
                DocumentsAvailableDescription.PlansAndSpecs,
                DocumentsAvailableDescription.BuildingPlans,
                DocumentsAvailableDescription.MUDWaterDistrict,
            };

            var localProperty = new LocalPropertyInfo(documents)
            {
                OpenHouseType = OpenHouseType.Friday,
                MarketStatus = MarketStatuses.Active,
            };

            var updatedDocuments = new HashSet<DocumentsAvailableDescription>
            {
                DocumentsAvailableDescription.PlansAndSpecs,
                DocumentsAvailableDescription.BuildingPlans,
            };
            var updatedLocalProperty = new LocalPropertyInfo(updatedDocuments)
            {
                OpenHouseType = OpenHouseType.Saturday,
                MarketStatus = MarketStatuses.Pending,
            };

            var expectedResult = new HashSet<HashSet<string>>
            {
                new HashSet<string>
                {
                    DocumentsAvailableDescription.BuildingPlans.ToStringFromEnumMember(),
                    DocumentsAvailableDescription.PlansAndSpecs.ToStringFromEnumMember(),
                },
                new HashSet<string> { MarketStatuses.Pending.ToString() },
                new HashSet<string> { updatedLocalProperty.OpenHouseType.ToStringFromEnumMember() },
            };

            // Act
            var result = SummaryExtensions.GetFieldSummary(updatedLocalProperty, localProperty, isInnerSummary: true).ToList();
            var resultNewValues = result.Select(x => x.NewValue).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(resultNewValues, expectedResult);
        }

        [Fact]
        public void GetFieldSummaryNewCollectionsObjectsSuccess()
        {
            var myString = "string";

            var documents = new HashSet<DocumentsAvailableDescription>
            {
                DocumentsAvailableDescription.PlansAndSpecs,
                DocumentsAvailableDescription.BuildingPlans,
                DocumentsAvailableDescription.MUDWaterDistrict,
            };

            var updatedLocalProperty = new LocalPropertyInfo(documents)
            {
                OpenHouseType = OpenHouseType.Saturday,
                MarketStatus = MarketStatuses.Pending,
                MyString = myString,
            };

            var expectedCollectionResult = new HashSet<HashSet<string>>
            {
                new HashSet<string>
                {
                    DocumentsAvailableDescription.PlansAndSpecs.ToStringFromEnumMember(),
                    DocumentsAvailableDescription.BuildingPlans.ToStringFromEnumMember(),
                    DocumentsAvailableDescription.MUDWaterDistrict.ToStringFromEnumMember(),
                },
                new HashSet<string> { MarketStatuses.Pending.ToString() },
                new HashSet<string> { updatedLocalProperty.OpenHouseType.ToStringFromEnumMember() },
            };

            // Act
            var result = SummaryExtensions.GetFieldSummary(updatedLocalProperty, (LocalPropertyInfo)null, isInnerSummary: true).ToList();
            var resultNewValues = result.Select(x => x.NewValue).ToList();
            var resultCollections = resultNewValues.Where(x => x is ICollection).ToList();
            var resultString = resultNewValues.SingleOrDefault(x => x is string);

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(resultCollections, expectedCollectionResult);
            Assert.Equal(myString, resultString);
        }

        private sealed record LocalPropertyInfo
        {
            public LocalPropertyInfo(IEnumerable<DocumentsAvailableDescription> documentsAvailable)
                : this()
            {
                this.DocumentsAvailable = documentsAvailable.ToList();
            }

            public LocalPropertyInfo(string myString)
                : this()
            {
                this.MyString = myString;
            }

            public LocalPropertyInfo(bool myBoolean)
                : this()
            {
                this.MyBoolean = myBoolean;
            }

            private LocalPropertyInfo()
            {
                this.DocumentsAvailable = new HashSet<DocumentsAvailableDescription>();
            }

            public ICollection<DocumentsAvailableDescription> DocumentsAvailable { get; set; }

            public MarketStatuses MarketStatus { get; set; }

            public OpenHouseType OpenHouseType { get; set; }

            public string MyString { get; set; }
            public bool MyBoolean { get; set; }
        }
    }
}
