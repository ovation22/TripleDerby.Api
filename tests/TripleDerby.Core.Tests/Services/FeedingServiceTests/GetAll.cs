using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.DTOs;
using Xunit;

namespace TripleDerby.Core.Tests.Services.FeedingServiceTests
{
    public class GetAll : FeedingServiceTestBase
    {
        [Fact]
        public async Task ItReturnsFeedings()
        {
            // Arrange
            // Act
            var feedings = await Service.GetAll();

            // Assert
            Assert.NotNull(feedings);
            Assert.IsAssignableFrom<IEnumerable<FeedingsResult>>(feedings);
        }

        [Fact]
        public async Task ItGetsFromCache()
        {
            // Arrange
            // Act
            await Service.GetAll();

            // Assert
            Cache.Verify(x => x.GetOrCreate(It.IsAny<string>(), It.IsAny<Func<Task<IEnumerable<FeedingsResult>>>>()), Times.Once());
        }
    }
}
