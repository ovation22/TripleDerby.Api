using System;
using System.Threading.Tasks;
using TripleDerby.Core.DTOs;
using Xunit;

namespace TripleDerby.Core.Tests.Services.RaceServiceTests
{
    public class Race : RaceServiceTestBase
    {
        private readonly byte _raceId;
        private readonly Guid _horseId;

        public Race()
        {
            _raceId = 9;
            _horseId = new Guid("B703FC6F-B72D-4048-9FEA-5264A50F8363");
        }

        [Fact]
        public async Task ItReturnsRaceRunResult()
        {
            // Arrange
            // Act
            var result = await Service.Race(_raceId, _horseId);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<RaceRunResult>(result);
        }
    }
}