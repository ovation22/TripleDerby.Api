using System.Threading.Tasks;
using TripleDerby.Core.Entities;
using TripleDerby.Infrastructure.Data;
using TripleDerby.Infrastructure.Data.Repositories;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories.EFRepositoryTests
{
    [Trait("Category", "EFRepository")]
    public class Add : EFRepositoryTestBase
    {
        [Fact]
        public async Task ItAddsHorse()
        {
            // Arrange
            await using var context = new TripleDerbyContext(Options);
            var repository = new TripleDerbyRepository(context);
            var horse = new Horse();

            // Act
            await repository.Add(horse);

            // Assert
            Assert.Contains(context.Horses, x => x == horse);
        }

        [Fact]
        public async Task ItReturnsNewlyAddedHorse()
        {
            // Arrange
            await using var context = new TripleDerbyContext(Options);
            var repository = new TripleDerbyRepository(context);
            var horse = new Horse();

            // Act
            var newlyAddedHorse = await repository.Add(horse);

            // Assert
            Assert.Equal(horse, newlyAddedHorse);
        }
    }
}