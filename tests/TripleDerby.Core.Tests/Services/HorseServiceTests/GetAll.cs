using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
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
            var horses = await Service.GetAll();

            // Assert
            Assert.NotNull(horses);
            Assert.IsAssignableFrom<IEnumerable<HorsesResult>>(horses);
        }

        [Fact]
        public async Task ItCallsRepository()
        {
            // Arrange
            // Act
            await Service.GetAll();

            // Assert
            Repository.Verify(x => x.GetAll<Horse>(), Times.Once());
        }
    }
}
