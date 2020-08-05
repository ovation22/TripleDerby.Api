using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
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
                    Id = _id
                };

            Repository.Setup(x => x.Get(It.IsAny<Expression<Func<Horse, bool>>>())).ReturnsAsync(_horse);
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