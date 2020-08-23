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
        public async Task ItCallsRepository()
        {
            // Arrange
            // Act
            await Service.GetAll();

            // Assert
            Repository.Verify(x => x.GetAll<Feeding>(), Times.Once());
        }
    }
}
