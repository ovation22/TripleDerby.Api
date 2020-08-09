using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace TripleDerby.Api.Tests.Controllers.BreedingControllerTests
{
    public class GetDams : BreedingControllerTestBase
    {
        [Fact]
        public async Task ItReturnsOkObjectResult()
        {
            // Arrange
            // Act
            var result = await Controller.GetDams();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ItGetsDams()
        {
            // Arrange
            // Act
            await Controller.GetDams();

            // Assert
            BreedingService.Verify(x => x.GetDams(), Times.Once());
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            BreedingService.Setup(x => x.GetDams()).Throws(new Exception());

            // Act
            var result = await Controller.GetDams();

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GivenException_ThenItLogsError()
        {
            // Arrange
            var ex = new Exception();
            BreedingService.Setup(x => x.GetDams()).Throws(ex);

            // Act
            await Controller.GetDams();

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}
