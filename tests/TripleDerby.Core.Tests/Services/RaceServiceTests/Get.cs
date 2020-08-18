using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Specifications;
using Xunit;

namespace TripleDerby.Core.Tests.Services.RaceServiceTests
{
    public class Get : RaceServiceTestBase
    {
        private readonly byte _id;
        private readonly Race _race;

        public Get()
        {
            _id = 4;
            _race = new Race
                {
                    Id = _id,
                    Track = new Track(),
                    Surface = new Surface()
                };

            Repository.Setup(x => x.Get(It.IsAny<RaceSpecification>())).ReturnsAsync(_race);
        }

        [Fact]
        public async Task ItReturnsRace()
        {
            // Arrange
            // Act
            var race = await Service.Get(_id);

            // Assert
            Assert.NotNull(race);
            Assert.IsAssignableFrom<RaceResult>(race);
        }
    }
}