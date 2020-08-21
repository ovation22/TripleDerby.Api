using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.DTOs;
using Xunit;

namespace TripleDerby.Core.Tests.Services.RaceServiceTests
{
    public class GetAll : RaceServiceTestBase
    {
        [Fact]
        public async Task ItReturnsRaces()
        {
            // Arrange
            // Act
            var races = await Service.GetAll();

            // Assert
            Assert.NotNull(races);
            Assert.IsAssignableFrom<IEnumerable<RacesResult>>(races);
        }

        [Fact]
        public async Task ItCallsRepository()
        {
            // Arrange
            // Act
            await Service.GetAll();

            // Assert
            Repository.Verify(x => x.GetAll<Entities.Race>(), Times.Once());
        }
    }
}
