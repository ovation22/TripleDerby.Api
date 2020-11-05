using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace TripleDerby.Api.Tests.Controllers.TrainingsControllerTests
{
    public class Train : TrainingsControllerTestBase
    {
        private readonly Guid _horseId;
        private readonly byte _trainingId;

        public Train()
        {
            _trainingId = 4;
            _horseId = new Guid("817B437E-E94E-4873-9C73-3C3C8B4F3901");
        }

        [Fact]
        public async Task ItReturnsOkResult()
        {
            // Arrange
            // Act
            var result = await Controller.Train(_trainingId, _horseId);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            TrainingService.Setup(x => x.Train(_trainingId, _horseId)).Throws(new Exception());

            // Act
            var result = await Controller.Train(_trainingId, _horseId);

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GivenException_ThenItLogsError()
        {
            // Arrange
            var ex = new Exception();
            TrainingService.Setup(x => x.Train(_trainingId, _horseId)).Throws(ex);

            // Act
            await Controller.Train(_trainingId, _horseId);

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}