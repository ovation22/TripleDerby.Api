using System;
using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Enums;
using Xunit;

namespace TripleDerby.Core.Tests.Services.BreedingServiceTests
{
    [Trait("Category", "Breeding")]
    public class Breed : BreedingServiceTestBase
    {
        private Horse _newHorse;
        private readonly Guid _damId;
        private readonly Guid _userId;
        private readonly Guid _sireId;

        public Breed()
        {
            _newHorse = new Horse();
            _damId = new Guid("7B48977C-754D-4463-B811-66DFCF5B4487");
            _userId = new Guid("E54F40AB-01A5-4ABB-9376-F196005F7259");
            _sireId = new Guid("FF55C438-DA12-48BC-A9D2-A6924335C8E6");

            Repository.Setup(x => x.Add(It.IsAny<Horse>()))
                .Callback<Horse>(x => _newHorse = x)
                .ReturnsAsync(new Horse());
        }

        [Fact]
        public async Task ItReturnsFoal()
        {
            // Arrange
            // Act
            var foal = await Service.Breed(_userId, _damId, _sireId);

            // Assert
            Assert.NotNull(foal);
            Assert.IsAssignableFrom<Foal>(foal);
        }

        [Fact]
        public async Task ItSetsHorseName()
        {
            // Arrange
            // Act
            await Service.Breed(_userId, _damId, _sireId);

            // Assert
            Assert.Equal("TODO", _newHorse.Name);
        }

        [Fact]
        public async Task ItSetsHorseColor()
        {
            // Arrange
            // Act
            await Service.Breed(_userId, _damId, _sireId);

            // Assert
            Assert.Equal(Color.Bay, _newHorse.Color);
        }
    }
}
