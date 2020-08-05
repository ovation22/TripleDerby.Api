using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace TripleDerby.Api.Tests.Controllers.HorsesControllerTests
{
    public class Counts : HorsesControllerTestBase
    {
        private readonly Guid _id;

        public Counts()
        {
            _id = new Guid("170E1003-5346-4E57-8721-FBBDFBC28EF6");
        }

        [Fact]
        public async Task ItReturnsOkObjectResult()
        {
            // Arrange
            // Act
            var result = await Controller.Get(_id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ItGetsHorse()
        {
            // Arrange
            // Act
            await Controller.Get(_id);

            // Assert
            HorseService.Verify(x => x.Get(_id), Times.Once());
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            HorseService.Setup(x => x.Get(_id)).Throws(new Exception());

            // Act
            var result = await Controller.Get(_id);

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GivenException_ThenItLogsError()
        {
            // Arrange
            var ex = new Exception();
            HorseService.Setup(x => x.Get(_id)).Throws(ex);

            // Act
            await Controller.Get(_id);

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}
