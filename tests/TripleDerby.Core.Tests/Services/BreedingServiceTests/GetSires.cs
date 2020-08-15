using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.Specification;
using Moq;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using Xunit;

namespace TripleDerby.Core.Tests.Services.BreedingServiceTests
{
    public class GetSires : BreedingServiceTestBase
    {
        [Fact]
        public async Task ItReturnsParentHorses()
        {
            // Arrange
            // Act
            var horses = await Service.GetSires();

            // Assert
            Assert.NotNull(horses);
            Assert.IsAssignableFrom<IEnumerable<ParentHorse>>(horses);
        }

        [Fact]
        public async Task ItCallsRepository()
        {
            // Arrange
            // Act
            await Service.GetSires();

            // Assert
            Repository.Verify(x => x.List(It.IsAny<Specification<Horse>>()), Times.Once());
        }
    }
}
