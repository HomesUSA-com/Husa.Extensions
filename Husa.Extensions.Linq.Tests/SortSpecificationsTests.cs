namespace Husa.Extensions.Linq.Tests
{
    using System;
    using System.Linq;
    using Husa.Extensions.Linq.Specifications;
    using Xunit;

    public class SortSpecificationsTests
    {
        private readonly DateTime[] dates = new[]
           {
                DateTime.UtcNow.AddMonths(-1),
                DateTime.UtcNow.AddMonths(-2),
                DateTime.UtcNow.AddMonths(-3),
           };

        [Fact]
        public void ApplySortByFields_Desc_Success()
        {
            // Arrange
            var sortFilter = "-sysModifiedOn";

            var listings = new[]
            {
                new
                {
                    SysModifiedOn = this.dates[1],
                },
                new
                {
                    SysModifiedOn = this.dates[2],
                },
                new
                {
                    SysModifiedOn = this.dates[0],
                },
            };

            // Act
            var sortedDates = listings.AsQueryable().ApplySortByFields(sortFilter).Select(x => x.SysModifiedOn);

            // Assert
            Assert.Equal(this.dates, sortedDates);
        }

        [Fact]
        public void ApplySortByFields_Asc_Success()
        {
            // Arrange
            var sortFilter = "+sysModifiedOn";

            var listings = new[]
            {
                new
                {
                    SysModifiedOn = this.dates[1],
                },
                new
                {
                    SysModifiedOn = this.dates[2],
                },
                new
                {
                    SysModifiedOn = this.dates[0],
                },
            };

            // Act
            var sortedDates = listings.AsQueryable().ApplySortByFields(sortFilter).Select(x => x.SysModifiedOn).Reverse();

            // Assert
            Assert.Equal(this.dates, sortedDates);
        }

        [Fact]
        public void ApplySortByFields_KeepSame()
        {
            // Arrange
            var sortFilter = "sysModifiedOn";

            var listings = new[]
            {
                new
                {
                    SysModifiedOn = this.dates[1],
                },
                new
                {
                    SysModifiedOn = this.dates[2],
                },
                new
                {
                    SysModifiedOn = this.dates[0],
                },
            };

            // Act
            var localDates = listings.Select(x => x.SysModifiedOn);
            var sortedDates = listings.AsQueryable().ApplySortByFields(sortFilter).Select(x => x.SysModifiedOn);

            // Assert
            Assert.Equal(localDates, sortedDates);
        }
    }
}
