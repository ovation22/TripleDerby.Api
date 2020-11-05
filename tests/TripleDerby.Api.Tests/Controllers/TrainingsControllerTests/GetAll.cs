using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace TripleDerby.Api.Tests.Controllers.TrainingsControllerTests
{
    public class GetAll : TrainingsControllerTestBase
    {
        [Fact]
        public async Task ItReturnsOkObjectResult()
        {
            // Arrange
            // Act
            var result = await Controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task ItGetsTrainings()
        {
            // Arrange
            // Act
            await Controller.GetAll();

            // Assert
            TrainingService.Verify(x => x.GetAll(), Times.Once());
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            TrainingService.Setup(x => x.GetAll()).Throws(new Exception());

            // Act
            var result = await Controller.GetAll();

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GivenException_ThenItLogsError()
        {
            // Arrange
            var ex = new Exception();
            TrainingService.Setup(x => x.GetAll()).Throws(ex);

            // Act
            await Controller.GetAll();

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}
