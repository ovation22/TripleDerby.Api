using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Specifications;
using Xunit;

namespace TripleDerby.Core.Tests.Services.HorseServiceTests
{
    public class GetAll : HorseServiceTestBase
    {
        [Fact]
        public async Task ItReturnsHorses()
        {
            // Arrange
            // Act
            var horses = await Service.GetAll(0, 1);

            // Assert
            Assert.NotNull(horses);
            Assert.IsAssignableFrom<HorsesResult>(horses);
        }

        [Fact]
        public async Task ItCallsRepository()
        {
            // Arrange
            // Act
            await Service.GetAll(0, 1);

            // Assert
            Repository.Verify(x => x.List(It.IsAny<HorsesPaginatedSpecification>()), Times.Once());
        }
    }
}
