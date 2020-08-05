using System;
using TripleDerby.Infrastructure.Utilities;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Utilities.TimeManagerTests
{
    [Trait("Category", "TimeManager")]
    public class Now
    {
        [Fact]
        public void ItReturnsLocalDateTime()
        {
            // Arrange
            var timeManager = new TimeManager();

            // Act 
            var result = timeManager.Now();

            // Assert
            Assert.IsAssignableFrom<DateTime>(result);
            Assert.Equal(DateTimeKind.Local, result.Kind);
        }
    }
}
