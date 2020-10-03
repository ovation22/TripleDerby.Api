using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using Xunit;

namespace TripleDerby.Core.Tests.Services.FeedingServiceTests
{
    public class GetAll : FeedingServiceTestBase
    {
        public GetAll()
        {
            Repository.Setup(x => x.GetAll<Feeding>()).ReturnsAsync(new List<Feeding>());
            Cache.Setup(x => x.GetOrCreate(It.IsAny<string>(), It.IsAny<Func<Task<IEnumerable<FeedingsResult>>>>()));
        }

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

        [Fact]
        public async Task ItGetsFromRepository()
        {
            // Arrange
            Cache.Setup(x => x.GetOrCreate(It.IsAny<string>(), It.IsAny<Func<Task<IEnumerable<FeedingsResult>>>>()))
                .Callback((string key, Func<Task<IEnumerable<FeedingsResult>>> action) => action());

            // Act
            await Service.GetAll();

            // Assert
            Repository.Verify(x => x.GetAll<Feeding>(), Times.Once());
        }
    }
}
