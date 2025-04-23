namespace Husa.Extensions.Common.Tests
{
    using System;
    using Husa.Extensions.Common.Enums;
    using Xunit;

    public class DateTimeExtensionsTests
    {
        [Fact]
        public void DateCompare_NullDates_ReturnsFalse()
        {
            // Arrange
            DateTime? date = null;
            DateTime? dateToCompare = DateTime.UtcNow;

            // Act & Assert
            Assert.False(date.DateCompare(OperatorType.Equal, dateToCompare));

            // Arrange
            date = DateTime.UtcNow;
            dateToCompare = null;

            // Act & Assert
            Assert.False(date.DateCompare(OperatorType.Equal, dateToCompare));

            // Arrange
            date = null;
            dateToCompare = null;

            // Act & Assert
            Assert.False(date.DateCompare(OperatorType.Equal, dateToCompare));
        }

        [Fact]
        public void DateCompare_LessThan_ReturnsCorrectResult()
        {
            // Arrange
            DateTime? date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime? dateToCompare = new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc);

            // Act & Assert
            Assert.True(date.DateCompare(OperatorType.LessThan, dateToCompare));
            Assert.False(dateToCompare.DateCompare(OperatorType.LessThan, date));
            Assert.False(date.DateCompare(OperatorType.LessThan, date)); // Same date
        }

        [Fact]
        public void DateCompare_LessEqual_ReturnsCorrectResult()
        {
            // Arrange
            DateTime? date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime? dateToCompare = new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc);

            // Act & Assert
            Assert.True(date.DateCompare(OperatorType.LessEqual, dateToCompare));
            Assert.False(dateToCompare.DateCompare(OperatorType.LessEqual, date));
            Assert.True(date.DateCompare(OperatorType.LessEqual, date)); // Same date
        }

        [Fact]
        public void DateCompare_GreaterThan_ReturnsCorrectResult()
        {
            // Arrange
            DateTime? date = new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc);
            DateTime? dateToCompare = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Act & Assert
            Assert.True(date.DateCompare(OperatorType.GreaterThan, dateToCompare));
            Assert.False(dateToCompare.DateCompare(OperatorType.GreaterThan, date));
            Assert.False(date.DateCompare(OperatorType.GreaterThan, date)); // Same date
        }

        [Fact]
        public void DateCompare_GreaterEqual_ReturnsCorrectResult()
        {
            // Arrange
            DateTime? date = new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc);
            DateTime? dateToCompare = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Act & Assert
            Assert.True(date.DateCompare(OperatorType.GreaterEqual, dateToCompare));
            Assert.False(dateToCompare.DateCompare(OperatorType.GreaterEqual, date));
            Assert.True(date.DateCompare(OperatorType.GreaterEqual, date)); // Same date
        }

        [Fact]
        public void DateCompare_EqualToTomorrow()
        {
            // Arrange
            var date = new DateTime(2025, 4, 24, 0, 0, 0, DateTimeKind.Utc);
            var dateUtc = date.AddHours(-DateTime.Now.GetOffsetHours());

            var today = DateTimeExtensions.TodayUtc();
            var tomorrow = today.AddDays(1);

            // Act & Assert
            Assert.False(dateUtc.DateCompare(OperatorType.GreaterThan, tomorrow));
            Assert.True(dateUtc.DateCompare(OperatorType.Equal, tomorrow));
        }

        [Fact]
        public void DateCompare_Equal_ReturnsCorrectResult()
        {
            // Arrange
            DateTime? date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime? differentDate = new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc);

            // Act & Assert
            Assert.True(date.DateCompare(OperatorType.Equal, date));
            Assert.False(date.DateCompare(OperatorType.Equal, differentDate));
        }

        [Fact]
        public void DateCompare_NotEqual_ReturnsCorrectResult()
        {
            // Arrange
            DateTime? date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime? differentDate = new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc);

            // Act & Assert
            Assert.False(date.DateCompare(OperatorType.NotEqual, date));
            Assert.True(date.DateCompare(OperatorType.NotEqual, differentDate));
        }

        [Fact]
        public void DateCompare_InvalidOperator_ReturnsFalse()
        {
            // Arrange
            DateTime? date = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime? dateToCompare = new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Utc);

            // Act & Assert - using an operator value that doesn't exist in the enum
            Assert.False(date.DateCompare((OperatorType)999, dateToCompare));
        }

        [Fact]
        public void DateCompare_IgnoresTimeComponent()
        {
            // Arrange
            DateTime? date = new DateTime(2023, 1, 1, 10, 30, 0, DateTimeKind.Utc);
            DateTime? sameDate = new DateTime(2023, 1, 1, 15, 45, 0, DateTimeKind.Utc);

            // Act & Assert - should compare only the date part
            Assert.True(date.DateCompare(OperatorType.Equal, sameDate));
        }

        [Fact]
        public void DateCompare_CompareTimes()
        {
            // Arrange
            DateTime? date = new DateTime(2023, 1, 1, 10, 30, 0, DateTimeKind.Utc);
            DateTime? sameDate = new DateTime(2023, 1, 1, 15, 45, 0, DateTimeKind.Utc);

            // Act & Assert - should compare only the date part
            Assert.False(date.DateCompare(OperatorType.Equal, sameDate, compareTime: true));
        }

        [Fact]
        public void DateCompare_HandlesTimeZoneConversion_DateTimeKindLocal()
        {
            var localDate = new DateTime(2023, 1, 1, 20, 0, 0, DateTimeKind.Local);
            var nextDay = new DateTime(2023, 1, 1, 20, 0, 0, DateTimeKind.Utc);
            var nextDayUtc = nextDay.AddHours(-localDate.GetOffsetHours());

            // Act & Assert - should be equal after conversion to UTC
            Assert.True(localDate.DateCompare(OperatorType.Equal, nextDayUtc));
        }

        [Fact]
        public void DateCompare_HandlesTimeZoneConversion_DateTimeKindUnspecified()
        {
            // Arrange - create dates that would be the same day in UTC
            DateTime? cstDate = new DateTime(2023, 1, 1, 20, 0, 0, DateTimeKind.Unspecified);
            DateTime? nextDayUtc = new DateTime(2023, 1, 2, 2, 0, 0, DateTimeKind.Utc);

            // Act & Assert - should be equal after conversion to UTC
            Assert.True(cstDate.DateCompare(OperatorType.Equal, nextDayUtc));
        }
    }
}
