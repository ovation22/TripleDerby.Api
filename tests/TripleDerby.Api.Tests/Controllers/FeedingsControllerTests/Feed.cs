using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace TripleDerby.Api.Tests.Controllers.FeedingsControllerTests
{
    public class Feed : FeedingsControllerTestBase
    {
        private readonly Guid _horseId;
        private readonly byte _feedingId;

        public Feed()
        {
            _feedingId = 4;
            _horseId = new Guid("79F0B154-54D8-41AD-AE7F-44255869F831");
        }

        [Fact]
        public async Task ItReturnsOkResult()
        {
            // Arrange
            // Act
            var result = await Controller.Feed(_feedingId, _horseId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            FeedingService.Setup(x => x.Feed(_feedingId, _horseId)).Throws(new Exception());

            // Act
            var result = await Controller.Feed(_feedingId, _horseId);

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GivenException_ThenItLogsError()
        {
            // Arrange
            var ex = new Exception();
            FeedingService.Setup(x => x.Feed(_feedingId, _horseId)).Throws(ex);

            // Act
            await Controller.Feed(_feedingId, _horseId);

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}