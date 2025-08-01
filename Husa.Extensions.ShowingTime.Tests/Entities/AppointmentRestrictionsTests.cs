namespace Husa.Extensions.ShowingTime.Tests.ShowingTime
{
    using Husa.Extensions.ShowingTime.Enums;
    using Husa.Extensions.ShowingTime.Models;
    using Xunit;

    public class AppointmentRestrictionsTests
    {
        [Fact]
        public void Clone_WithAllProperties_Success()
        {
            // Arrange
            var original = GetAppointmentRestrictions();

            // Act
            var cloned = original.Clone();

            // Assert
            Assert.NotSame(original, cloned);
            Assert.Equal(original.AllowRealtimeAvailabilityForBrokers, cloned.AllowRealtimeAvailabilityForBrokers);
            Assert.Equal(original.AllowInspectionsAndWalkThroughs, cloned.AllowInspectionsAndWalkThroughs);
            Assert.Equal(original.AllowAppraisals, cloned.AllowAppraisals);
            Assert.Equal(original.RequiredTimeHours, cloned.RequiredTimeHours);
            Assert.Equal(original.SuggestedTimeHours, cloned.SuggestedTimeHours);
            Assert.Equal(original.MinShowingWindowShowings, cloned.MinShowingWindowShowings);
            Assert.Equal(original.MaxShowingWindowShowings, cloned.MaxShowingWindowShowings);
            Assert.Equal(original.BufferTimeBetweenAppointments, cloned.BufferTimeBetweenAppointments);
            Assert.Equal(original.OverlappingAppointmentMode, cloned.OverlappingAppointmentMode);
        }

        [Fact]
        public void Clone_WithNullableProperties_Success()
        {
            // Arrange
            var original = new AppointmentRestrictions
            {
                AllowRealtimeAvailabilityForBrokers = false,
                AllowInspectionsAndWalkThroughs = true,
                AllowAppraisals = false,
                RequiredTimeHours = null,
                SuggestedTimeHours = null,
                MinShowingWindowShowings = null,
                MaxShowingWindowShowings = null,
                BufferTimeBetweenAppointments = null,
                OverlappingAppointmentMode = null,
            };

            // Act
            var cloned = original.Clone();

            // Assert
            Assert.NotSame(original, cloned);
            Assert.Equal(original.RequiredTimeHours, cloned.RequiredTimeHours);
            Assert.Equal(original.SuggestedTimeHours, cloned.SuggestedTimeHours);
            Assert.Equal(original.MinShowingWindowShowings, cloned.MinShowingWindowShowings);
            Assert.Equal(original.MaxShowingWindowShowings, cloned.MaxShowingWindowShowings);
            Assert.Equal(original.BufferTimeBetweenAppointments, cloned.BufferTimeBetweenAppointments);
            Assert.Equal(original.OverlappingAppointmentMode, cloned.OverlappingAppointmentMode);
        }

        [Fact]
        public void GetEqualityComponents_SameValues_AreEqual()
        {
            // Arrange
            var restrictions1 = GetAppointmentRestrictions();

            var restrictions2 = GetAppointmentRestrictions();

            // Act & Assert
            Assert.Equal(restrictions1, restrictions2);
        }

        [Fact]
        public void GetEqualityComponents_DifferentAllowAppraisals_AreNotEqual()
        {
            // Arrange
            var restrictions1 = new AppointmentRestrictions
            {
                AllowAppraisals = true,
            };

            var restrictions2 = new AppointmentRestrictions
            {
                AllowAppraisals = false,
            };

            // Act & Assert
            Assert.NotEqual(restrictions1, restrictions2);
        }

        [Fact]
        public void GetEqualityComponents_DifferentAllowInspectionsAndWalkThroughs_AreNotEqual()
        {
            // Arrange
            var restrictions1 = new AppointmentRestrictions
            {
                AllowInspectionsAndWalkThroughs = true,
            };

            var restrictions2 = new AppointmentRestrictions
            {
                AllowInspectionsAndWalkThroughs = false,
            };

            // Act & Assert
            Assert.NotEqual(restrictions1, restrictions2);
        }

        [Fact]
        public void GetEqualityComponents_DifferentRequiredTimeHours_AreNotEqual()
        {
            // Arrange
            var restrictions1 = new AppointmentRestrictions
            {
                RequiredTimeHours = AppointmentTimeHours.TwentyFour,
            };

            var restrictions2 = new AppointmentRestrictions
            {
                RequiredTimeHours = AppointmentTimeHours.FortyEight,
            };

            // Act & Assert
            Assert.NotEqual(restrictions1, restrictions2);
        }

        [Fact]
        public void GetEqualityComponents_DifferentSuggestedTimeHours_AreNotEqual()
        {
            // Arrange
            var restrictions1 = new AppointmentRestrictions
            {
                SuggestedTimeHours = AppointmentTimeHours.TwentyFour,
            };

            var restrictions2 = new AppointmentRestrictions
            {
                SuggestedTimeHours = AppointmentTimeHours.FortyEight,
            };

            // Act & Assert
            Assert.NotEqual(restrictions1, restrictions2);
        }

        [Fact]
        public void GetEqualityComponents_DifferentAllowRealtimeAvailabilityForBrokers_AreNotEqual()
        {
            // Arrange
            var restrictions1 = new AppointmentRestrictions
            {
                AllowRealtimeAvailabilityForBrokers = true,
            };

            var restrictions2 = new AppointmentRestrictions
            {
                AllowRealtimeAvailabilityForBrokers = false,
            };

            // Act & Assert
            Assert.NotEqual(restrictions1, restrictions2);
        }

        [Fact]
        public void GetEqualityComponents_DifferentMinShowingWindowShowings_AreNotEqual()
        {
            // Arrange
            var restrictions1 = new AppointmentRestrictions
            {
                MinShowingWindowShowings = AppointmentLength.OneHour,
            };

            var restrictions2 = new AppointmentRestrictions
            {
                MinShowingWindowShowings = AppointmentLength.ThirtyMinutes,
            };

            // Act & Assert
            Assert.NotEqual(restrictions1, restrictions2);
        }

        [Fact]
        public void GetEqualityComponents_DifferentMaxShowingWindowShowings_AreNotEqual()
        {
            // Arrange
            var restrictions1 = new AppointmentRestrictions
            {
                MaxShowingWindowShowings = AppointmentLength.FifteenMinutes,
            };

            var restrictions2 = new AppointmentRestrictions
            {
                MaxShowingWindowShowings = AppointmentLength.ThirtyMinutes,
            };

            // Act & Assert
            Assert.NotEqual(restrictions1, restrictions2);
        }

        [Fact]
        public void GetEqualityComponents_DifferentBufferTimeBetweenAppointments_AreNotEqual()
        {
            // Arrange
            var restrictions1 = new AppointmentRestrictions
            {
                BufferTimeBetweenAppointments = TimeBetweenAppointments.FifteenMinutes,
            };

            var restrictions2 = new AppointmentRestrictions
            {
                BufferTimeBetweenAppointments = TimeBetweenAppointments.ThirtyMinutes,
            };

            // Act & Assert
            Assert.NotEqual(restrictions1, restrictions2);
        }

        [Fact]
        public void GetEqualityComponents_DifferentOverlappingAppointmentMode_AreNotEqual()
        {
            // Arrange
            var restrictions1 = new AppointmentRestrictions
            {
                OverlappingAppointmentMode = OverlappingAppointmentMode.Default,
            };

            var restrictions2 = new AppointmentRestrictions
            {
                OverlappingAppointmentMode = OverlappingAppointmentMode.AllowNoWarning,
            };

            // Act & Assert
            Assert.NotEqual(restrictions1, restrictions2);
        }

        [Fact]
        public void GetEqualityComponents_NullVsValue_AreNotEqual()
        {
            // Arrange
            var restrictions1 = new AppointmentRestrictions
            {
                RequiredTimeHours = null,
            };

            var restrictions2 = new AppointmentRestrictions
            {
                RequiredTimeHours = AppointmentTimeHours.TwentyFour,
            };

            // Act & Assert
            Assert.NotEqual(restrictions1, restrictions2);
        }

        [Fact]
        public void GetEqualityComponents_BothNull_AreEqual()
        {
            // Arrange
            var restrictions1 = new AppointmentRestrictions
            {
                RequiredTimeHours = null,
                SuggestedTimeHours = null,
                MinShowingWindowShowings = null,
                MaxShowingWindowShowings = null,
                BufferTimeBetweenAppointments = null,
                OverlappingAppointmentMode = null,
            };

            var restrictions2 = new AppointmentRestrictions
            {
                RequiredTimeHours = null,
                SuggestedTimeHours = null,
                MinShowingWindowShowings = null,
                MaxShowingWindowShowings = null,
                BufferTimeBetweenAppointments = null,
                OverlappingAppointmentMode = null,
            };

            // Act & Assert
            Assert.Equal(restrictions1, restrictions2);
        }

        private static AppointmentRestrictions GetAppointmentRestrictions() => new()
            {
                AllowRealtimeAvailabilityForBrokers = true,
                AllowInspectionsAndWalkThroughs = false,
                AllowAppraisals = true,
                RequiredTimeHours = AppointmentTimeHours.TwentyFour,
                SuggestedTimeHours = AppointmentTimeHours.FortyEight,
                MinShowingWindowShowings = AppointmentLength.TwoHours,
                MaxShowingWindowShowings = AppointmentLength.ThirtyMinutes,
                BufferTimeBetweenAppointments = TimeBetweenAppointments.FifteenMinutes,
                OverlappingAppointmentMode = OverlappingAppointmentMode.AllowNoWarning,
            };
}
}
