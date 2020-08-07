using System.Threading.Tasks;
using TripleDerby.Core.Entities;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories.EFRepositoryTests
{
    [Collection("ContextFixture")]
    [Trait("Category", "EFRepository")]
    public class Add : EFRepositoryTestBase
    {
        public Add(ContextFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task ItAddsHorse()
        {
            // Arrange
            var horse = new Horse();

            // Act
            await Repository.Add(horse);

            // Assert
            Assert.Contains(Context.Horses, x => x == horse);
        }

        [Fact]
        public async Task ItReturnsNewlyAddedHorse()
        {
            // Arrange
            var horse = new Horse();

            // Act
            var newlyAddedHorse = await Repository.Add(horse);

            // Assert
            Assert.Equal(horse, newlyAddedHorse);
        }
    }
}