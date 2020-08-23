using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Specifications;
using Xunit;

namespace TripleDerby.Core.Tests.Services.RaceServiceTests
{
    public class Race : RaceServiceTestBase
    {
        private readonly byte _raceId;
        private readonly Guid _horseId;
        private RaceRun _raceRun = default!;

        public Race()
        {
            _raceId = 9;
            _horseId = new Guid("B703FC6F-B72D-4048-9FEA-5264A50F8363");

            Repository.Setup(x => x.List(It.IsAny<HorseRandomRacerSpecification>())).ReturnsAsync(new List<Horse>());
            Repository.Setup(x => x.Add(It.IsAny<RaceRun>())).Callback<RaceRun>(x => _raceRun = x);
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

        [Fact]
        public async Task ItGetsRandomRacers()
        {
            // Arrange
            // Act
            await Service.Race(_raceId, _horseId);

            // Assert
            Repository.Verify(x => x.List(It.IsAny<HorseRandomRacerSpecification>()), Times.Once);
        }

        [Fact]
        public async Task ItCreatesRaceRun()
        {
            // Arrange
            // Act
            await Service.Race(_raceId, _horseId);

            // Assert
            Repository.Verify(x => x.Add(It.IsAny<RaceRun>()), Times.Once);
        }

        [Fact]
        public async Task UserHorseAddedToRaceRunHorses()
        {
            // Arrange
            // Act
            await Service.Race(_raceId, _horseId);

            // Assert
            Assert.Contains(_raceRun.Horses, x => x.HorseId == _horseId);
        }
    }
}