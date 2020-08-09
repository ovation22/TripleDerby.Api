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
        private Horse _newHorse;
        private readonly Guid _userId;

        public Breed()
        {
            _newHorse = new Horse();
            _userId = new Guid("E54F40AB-01A5-4ABB-9376-F196005F7259");

            Repository.Setup(x => x.Add(It.IsAny<Horse>()))
                .Callback<Horse>(x => _newHorse = x)
                .ReturnsAsync(new Horse());
        }

        [Fact]
        public async Task ItReturnsFoal()
        {
            // Arrange
            // Act
            var foal = await Service.Breed(_userId, DamId, SireId);

            // Assert
            Assert.NotNull(foal);
            Assert.IsAssignableFrom<Foal>(foal);
        }

        [Fact]
        public async Task ItSetsHorseName()
        {
            // Arrange
            // Act
            await Service.Breed(_userId, DamId, SireId);

            // Assert
            Assert.Equal("TODO", _newHorse.Name);
        }
    }
}
