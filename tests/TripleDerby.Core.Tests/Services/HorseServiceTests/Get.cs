using System;
using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Specifications;
using Xunit;

namespace TripleDerby.Core.Tests.Services.HorseServiceTests
{
    public class Get : HorseServiceTestBase
    {
        private readonly Guid _id;
        private readonly Horse _horse;

        public Get()
        {
            _id = new Guid("1BF95F24-0C0B-4C9A-8B80-46110CA9413E");
            _horse = new Horse
                {
                    Id = _id,
                    Color = new Color()
                };

            Repository.Setup(x => x.Get(It.IsAny<HorseSpecification>())).ReturnsAsync(_horse);
        }

        [Fact]
        public async Task ItReturnsHorse()
        {
            // Arrange
            // Act
            var horse = await Service.Get(_id);

            // Assert
            Assert.NotNull(horse);
            Assert.IsAssignableFrom<HorseResult>(horse);
        }
    }
}