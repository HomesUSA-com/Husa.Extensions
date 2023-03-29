namespace Husa.Extensions.Common.Tests
{
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
            var result = SummaryExtensions.GetFieldSummary(localProperty, updatedLocalProperty, isInnerSummary: true);

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
            public string MyString { get; set; }
            public bool MyBoolean { get; set; }
        }
    }
}
