using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace TripleDerby.Api.Tests.Controllers.FeedingsControllerTests
{
    public class GetAll : FeedingsControllerTestBase
    {
        [Fact]
        public async Task ItReturnsOkObjectResult()
        {
            // Arrange
            // Act
            var result = await Controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ItGetsFeedings()
        {
            // Arrange
            // Act
            await Controller.GetAll();

            // Assert
            FeedingService.Verify(x => x.GetAll(), Times.Once());
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            FeedingService.Setup(x => x.GetAll()).Throws(new Exception());

            // Act
            var result = await Controller.GetAll();

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GivenException_ThenItLogsError()
        {
            // Arrange
            var ex = new Exception();
            FeedingService.Setup(x => x.GetAll()).Throws(ex);

            // Act
            await Controller.GetAll();

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}
