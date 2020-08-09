using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TripleDerby.Core.DTOs;
using Xunit;

namespace TripleDerby.Api.Tests.Controllers.BreedingControllerTests
{
    public class Breed : BreedingControllerTestBase
    {
        private readonly BreedRequest _request;

        public Breed()
        {
            _request = new BreedRequest
            {
                UserId = new Guid("170E1003-5346-4E57-8721-FBBDFBC28EF6"),
                SireId = new Guid("4B1062E6-8C62-4E63-B9A1-A0260EC01B91"),
                DamId = new Guid("7D5F0D3C-A2AD-4CDD-9F06-6770569CF51F")
            };
        }

        [Fact]
        public async Task ItReturnsOkObjectResult()
        {
            // Arrange
            // Act
            var result = await Controller.Breed(_request);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ItBreedsFoal()
        {
            // Arrange
            // Act
            await Controller.Breed(_request);

            // Assert
            BreedingService.Verify(x => x.Breed(_request), Times.Once());
        }

        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            BreedingService.Setup(x => x.Breed(_request)).Throws(new Exception());

            // Act
            var result = await Controller.Breed(_request);

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GivenException_ThenItLogsError()
        {
            // Arrange
            var ex = new Exception();
            BreedingService.Setup(x => x.Breed(_request)).Throws(ex);

            // Act
            await Controller.Breed(_request);

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}
