namespace Husa.Extensions.ShowingTime.Tests.ShowingTime
{
    using Husa.Extensions.ShowingTime.Enums;
    using Husa.Extensions.ShowingTime.Models;
    using Xunit;

    public class AccessInformationTests
    {
        [Fact]
        public void Clone_Success()
        {
            // Arrange
            var original = new AccessInformation
            {
                GateCode = "GATE123",
                AccessMethod = AccessMethod.CodeBox,
                Location = "Front Door",
                Serial = "SER123456",
                Combination = "1234",
                SharingCode = "SHARE789",
                CbsCode = "CBS001",
                Code = "CODE456",
                DeviceId = "DEV789",
                AccessNotes = "Use side entrance",
                ProvideAlarmDetails = true,
                AlarmDisarmCode = "DISARM123",
                AlarmArmCode = "ARM456",
                AlarmPasscode = "PASS789",
                AlarmNotes = "Alarm is sensitive",
            };

            // Act
            var cloned = original.Clone();

            // Assert
            Assert.NotSame(original, cloned);
            Assert.Equal(original.GateCode, cloned.GateCode);
            Assert.Equal(original.AccessMethod, cloned.AccessMethod);
            Assert.Equal(original.Location, cloned.Location);
            Assert.Equal(original.Serial, cloned.Serial);
            Assert.Equal(original.Combination, cloned.Combination);
            Assert.Equal(original.SharingCode, cloned.SharingCode);
            Assert.Equal(original.CbsCode, cloned.CbsCode);
            Assert.Equal(original.Code, cloned.Code);
            Assert.Equal(original.DeviceId, cloned.DeviceId);
            Assert.Equal(original.AccessNotes, cloned.AccessNotes);
            Assert.Equal(original.ProvideAlarmDetails, cloned.ProvideAlarmDetails);
            Assert.Equal(original.AlarmDisarmCode, cloned.AlarmDisarmCode);
            Assert.Equal(original.AlarmArmCode, cloned.AlarmArmCode);
            Assert.Equal(original.AlarmPasscode, cloned.AlarmPasscode);
            Assert.Equal(original.AlarmNotes, cloned.AlarmNotes);
        }

        [Fact]
        public void Clone_WithNullValues_Success()
        {
            // Arrange
            var original = new AccessInformation
            {
                GateCode = null,
                AccessMethod = null,
                Location = null,
                Serial = null,
                Combination = null,
                SharingCode = null,
                CbsCode = null,
                Code = null,
                DeviceId = null,
                AccessNotes = null,
                ProvideAlarmDetails = false,
                AlarmDisarmCode = null,
                AlarmArmCode = null,
                AlarmPasscode = null,
                AlarmNotes = null,
            };

            // Act
            var cloned = original.Clone();

            // Assert
            Assert.NotSame(original, cloned);
            Assert.Equal(original, cloned);
        }

        [Fact]
        public void Clone_ModifyCloned_DoesNotAffectOriginal()
        {
            // Arrange
            var original = new AccessInformation
            {
                GateCode = "ORIGINAL123",
                AccessMethod = AccessMethod.CodeBox,
                ProvideAlarmDetails = true,
            };

            // Act
            var cloned = original.Clone();
            cloned.GateCode = "MODIFIED456";
            cloned.AccessMethod = AccessMethod.CodeBox;
            cloned.ProvideAlarmDetails = false;

            // Assert
            Assert.Equal("ORIGINAL123", original.GateCode);
            Assert.Equal(AccessMethod.CodeBox, original.AccessMethod);
            Assert.True(original.ProvideAlarmDetails);
            Assert.Equal("MODIFIED456", cloned.GateCode);
            Assert.Equal(AccessMethod.CodeBox, cloned.AccessMethod);
            Assert.False(cloned.ProvideAlarmDetails);
        }

        [Fact]
        public void GetEqualityComponents_SameValues_AreEqual()
        {
            // Arrange
            var accessInfo1 = new AccessInformation
            {
                GateCode = "GATE123",
                AccessMethod = AccessMethod.CodeBox,
                Location = "Front Door",
                Serial = "SER123456",
                Combination = "1234",
                SharingCode = "SHARE789",
                CbsCode = "CBS001",
                Code = "CODE456",
                DeviceId = "DEV789",
                AccessNotes = "Use side entrance",
                ProvideAlarmDetails = true,
                AlarmDisarmCode = "DISARM123",
                AlarmArmCode = "ARM456",
                AlarmPasscode = "PASS789",
                AlarmNotes = "Alarm is sensitive",
            };

            var accessInfo2 = new AccessInformation
            {
                GateCode = "GATE123",
                AccessMethod = AccessMethod.CodeBox,
                Location = "Front Door",
                Serial = "SER123456",
                Combination = "1234",
                SharingCode = "SHARE789",
                CbsCode = "CBS001",
                Code = "CODE456",
                DeviceId = "DEV789",
                AccessNotes = "Use side entrance",
                ProvideAlarmDetails = true,
                AlarmDisarmCode = "DISARM123",
                AlarmArmCode = "ARM456",
                AlarmPasscode = "PASS789",
                AlarmNotes = "Alarm is sensitive",
            };

            // Act & Assert
            Assert.Equal(accessInfo1, accessInfo2);
            Assert.True(accessInfo1.Equals(accessInfo2));
            Assert.Equal(accessInfo1.GetHashCode(), accessInfo2.GetHashCode());
        }

        [Fact]
        public void GetEqualityComponents_DifferentGateCode_AreNotEqual()
        {
            // Arrange
            var accessInfo1 = new AccessInformation { GateCode = "GATE123" };
            var accessInfo2 = new AccessInformation { GateCode = "GATE456" };

            // Act & Assert
            Assert.NotEqual(accessInfo1, accessInfo2);
            Assert.False(accessInfo1.Equals(accessInfo2));
        }

        [Fact]
        public void GetEqualityComponents_DifferentAccessMethod_AreNotEqual()
        {
            // Arrange
            var accessInfo1 = new AccessInformation { AccessMethod = AccessMethod.Keypad };
            var accessInfo2 = new AccessInformation { AccessMethod = AccessMethod.CodeBox };

            // Act & Assert
            Assert.NotEqual(accessInfo1, accessInfo2);
            Assert.False(accessInfo1.Equals(accessInfo2));
        }

        [Fact]
        public void GetEqualityComponents_DifferentProvideAlarmDetails_AreNotEqual()
        {
            // Arrange
            var accessInfo1 = new AccessInformation { ProvideAlarmDetails = true };
            var accessInfo2 = new AccessInformation { ProvideAlarmDetails = false };

            // Act & Assert
            Assert.NotEqual(accessInfo1, accessInfo2);
            Assert.False(accessInfo1.Equals(accessInfo2));
        }

        [Theory]
        [InlineData("Location1", "Location2")]
        [InlineData("Serial1", "Serial2")]
        [InlineData("Combo1", "Combo2")]
        public void GetEqualityComponents_DifferentStringProperties_AreNotEqual(string value1, string value2)
        {
            // Arrange
            var accessInfo1 = new AccessInformation
            {
                Location = value1,
                Serial = value1,
                Combination = value1,
            };
            var accessInfo2 = new AccessInformation
            {
                Location = value2,
                Serial = value1,
                Combination = value1,
            };

            // Act & Assert
            Assert.NotEqual(accessInfo1, accessInfo2);
        }

        [Fact]
        public void GetEqualityComponents_NullValues_AreEqual()
        {
            // Arrange
            var accessInfo1 = new AccessInformation();
            var accessInfo2 = new AccessInformation();

            // Act & Assert
            Assert.Equal(accessInfo1, accessInfo2);
            Assert.True(accessInfo1.Equals(accessInfo2));
            Assert.Equal(accessInfo1.GetHashCode(), accessInfo2.GetHashCode());
        }

        [Fact]
        public void GetEqualityComponents_OneNullOneNotNull_AreNotEqual()
        {
            // Arrange
            var accessInfo1 = new AccessInformation { GateCode = null };
            var accessInfo2 = new AccessInformation { GateCode = "GATE123" };

            // Act & Assert
            Assert.NotEqual(accessInfo1, accessInfo2);
            Assert.False(accessInfo1.Equals(accessInfo2));
        }

        [Fact]
        public void GetEqualityComponents_AllPropertiesDifferent_AreNotEqual()
        {
            // Arrange
            var accessInfo1 = new AccessInformation
            {
                GateCode = "GATE1",
                AccessMethod = AccessMethod.CodeBox,
                Location = "Location1",
                Serial = "Serial1",
                Combination = "Combo1",
                SharingCode = "Share1",
                CbsCode = "CBS1",
                Code = "Code1",
                DeviceId = "Device1",
                AccessNotes = "Notes1",
                ProvideAlarmDetails = true,
                AlarmDisarmCode = "Disarm1",
                AlarmArmCode = "Arm1",
                AlarmPasscode = "Pass1",
                AlarmNotes = "AlarmNotes1",
            };

            var accessInfo2 = new AccessInformation
            {
                GateCode = "GATE2",
                AccessMethod = AccessMethod.Combination,
                Location = "Location2",
                Serial = "Serial2",
                Combination = "Combo2",
                SharingCode = "Share2",
                CbsCode = "CBS2",
                Code = "Code2",
                DeviceId = "Device2",
                AccessNotes = "Notes2",
                ProvideAlarmDetails = false,
                AlarmDisarmCode = "Disarm2",
                AlarmArmCode = "Arm2",
                AlarmPasscode = "Pass2",
                AlarmNotes = "AlarmNotes2",
            };

            // Act & Assert
            Assert.NotEqual(accessInfo1, accessInfo2);
            Assert.False(accessInfo1.Equals(accessInfo2));
        }
    }
}
