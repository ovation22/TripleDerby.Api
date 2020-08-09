using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace TripleDerby.Api.Tests.Controllers.BreedingControllerTests
{
    public class GetSires : BreedingControllerTestBase
    {
        [Fact]
        public async Task ItReturnsOkObjectResult()
        {
            // Arrange
            // Act
            var result = await Controller.GetSires();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ItGetsSires()
        {
            // Arrange
            // Act
            await Controller.GetSires();

            // Assert
            BreedingService.Verify(x => x.GetSires(), Times.Once());
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            BreedingService.Setup(x => x.GetSires()).Throws(new Exception());

            // Act
            var result = await Controller.GetSires();

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GivenException_ThenItLogsError()
        {
            // Arrange
            var ex = new Exception();
            BreedingService.Setup(x => x.GetSires()).Throws(ex);

            // Act
            await Controller.GetSires();

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}
