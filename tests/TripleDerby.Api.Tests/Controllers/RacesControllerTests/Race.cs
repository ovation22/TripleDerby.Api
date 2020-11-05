using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace TripleDerby.Api.Tests.Controllers.RacesControllerTests
{
    public class Race : RacesControllerTestBase
    {
        private readonly byte _raceId;
        private readonly Guid _horseId;

        public Race()
        {
            _raceId = 4;
            _horseId = new Guid("6CF0764C-4445-42BF-ABC0-A39A429510F0");
        }

        [Fact]
        public async Task ItReturnsOkResult()
        {
            // Arrange
            // Act
            var result = await Controller.Race(_raceId, _horseId);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            RaceService.Setup(x => x.Race(_raceId, _horseId)).Throws(new Exception());

            // Act
            var result = await Controller.Race(_raceId, _horseId);

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GivenException_ThenItLogsError()
        {
            // Arrange
            var ex = new Exception();
            RaceService.Setup(x => x.Race(_raceId, _horseId)).Throws(ex);

            // Act
            await Controller.Race(_raceId, _horseId);

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}