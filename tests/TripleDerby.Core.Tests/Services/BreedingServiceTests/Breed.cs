using System;
using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using Xunit;

namespace TripleDerby.Core.Tests.Services.BreedingServiceTests
{
    [Trait("Category", "Breeding")]
    public class Breed : BreedingServiceTestBase
    {
        private readonly Guid _userId;
        private readonly Guid _damId;
        private readonly Guid _sireId;

        public Breed()
        {
            _userId = new Guid("E54F40AB-01A5-4ABB-9376-F196005F7259");
            _damId = new Guid("7B48977C-754D-4463-B811-66DFCF5B4487");
            _sireId = new Guid("FF55C438-DA12-48BC-A9D2-A6924335C8E6");
        }

        [Fact]
        public async Task ItReturnsFoal()
        {
            // Arrange
            var horse = new Horse();
            Repository.Setup(x => x.Add(It.IsAny<Horse>())).ReturnsAsync(horse);

            // Act
            var foal = await Service.Breed(_userId, _damId, _sireId);

            // Assert
            Assert.NotNull(foal);
            Assert.IsAssignableFrom<Foal>(foal);
        }
    }
}
