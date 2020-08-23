using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Specifications;
using Xunit;

namespace TripleDerby.Core.Tests.Services.FeedingServiceTests
{
    public class Get : FeedingServiceTestBase
    {
        private readonly byte _id;
        private readonly Feeding _feeding;

        public Get()
        {
            _id = 4;
            _feeding = new Feeding
                {
                    Id = _id
                };

            Repository.Setup(x => x.Get(It.IsAny<FeedingSpecification>())).ReturnsAsync(_feeding);
        }

        [Fact]
        public async Task ItReturnsFeeding()
        {
            // Arrange
            // Act
            var feeding = await Service.Get(_id);

            // Assert
            Assert.NotNull(feeding);
            Assert.IsAssignableFrom<FeedingResult>(feeding);
        }
    }
}