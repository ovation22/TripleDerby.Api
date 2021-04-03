using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TripleDerby.Core.DTOs;
using Xunit;

namespace TripleDerby.Api.Tests.Controllers.HorsesControllerTests
{
    public class Patch : HorsesControllerTestBase
    {
        private readonly Guid _id;
        private readonly JsonPatchDocument<HorsePatch> _patch;

        public Patch()
        {
            _id = new Guid("170E1003-5346-4E57-8721-FBBDFBC28EF6");
            _patch = new JsonPatchDocument<HorsePatch>();
        }

        [Fact]
        public async Task ItReturnsNoContentResult()
        {
            // Arrange
            // Act
            var result = await Controller.Patch(_id, _patch);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
        
        [Fact]
        public async Task GivenJsonPatchException_ThenBadRequestResult()
        {
            // Arrange
            HorseService.Setup(x => x.Update(_id, _patch)).Throws(new JsonPatchException());

            // Act
            var result = await Controller.Patch(_id, _patch);

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }
        
        [Fact]
        public async Task GivenException_ThenBadRequestResult()
        {
            // Arrange
            HorseService.Setup(x => x.Update(_id, _patch)).Throws(new Exception());

            // Act
            var result = await Controller.Patch(_id, _patch);

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GivenException_ThenItLogsError()
        {
            // Arrange
            var ex = new Exception();
            HorseService.Setup(x => x.Update(_id, _patch)).Throws(ex);

            // Act
            await Controller.Patch(_id, _patch);

            // Assert
            Logger.Verify(x => x.LogError(ex, ex.Message), Times.Once());
        }
    }
}
