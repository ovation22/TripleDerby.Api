using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace TripleDerby.Api.Tests.Controllers.HorsesControllerTests
{
    public class GetAll : HorsesControllerTestBase
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
        public async Task ItGetsHorses()
        {
            // Arrange
            // Act
            await Controller.GetAll();

            // Assert
            HorseService.Verify(x => x.GetAll(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            HorseService.Setup(x => x.GetAll(It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception());

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
            HorseService.Setup(x => x.GetAll(It.IsAny<int>(), It.IsAny<int>())).Throws(ex);

            // Act
            await Controller.GetAll();

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}
