using System.Threading.Tasks;
using TripleDerby.Core.Entities;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories.TripleDerbyRepositoryTests
{
    [Collection("ContextFixture")]
    [Trait("Category", "TripleDerbyRepository")]
    public class Add : TripleDerbyRepositoryTestBase
    {
        public Add(ContextFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task ItAddsHorse()
        {
            // Arrange
            var horse = new Horse
            {
                Name = "horse"
            };

            // Act
            Repository.Add(horse);
            await Repository.Save();

            // Assert
            Assert.Contains(Context.Horses, x => x == horse);
        }

        [Fact]
        public async Task ItReturnsNewlyAddedHorse()
        {
            // Arrange
            var horse = new Horse
            {
                Name = "newly"
            };

            // Act
            var newlyAddedHorse = Repository.Add(horse);
            await Repository.Save();

            // Assert
            Assert.Equal(horse, newlyAddedHorse);
        }
    }
}