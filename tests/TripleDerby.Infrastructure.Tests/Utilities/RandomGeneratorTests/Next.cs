using TripleDerby.Infrastructure.Utilities;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Utilities.RandomGeneratorTests
{
    [Trait("Category", "RandomGenerator")]
    public class Next
    {
        [Fact]
        public void ItReturnsRandomNext()
        {
            // Arrange
            var randomGenerator = new RandomGenerator();

            // Act 
            var result = randomGenerator.Next();

            // Assert
            Assert.IsAssignableFrom<int>(result);
        }

        [Fact]
        public void ItReturnsRandomNextLessThanMax()
        {
            // Arrange
            const int max = 2;
            var randomGenerator = new RandomGenerator();

            // Act 
            var result = randomGenerator.Next(max);

            // Assert
            Assert.InRange(result, 0, max);
        }

        [Fact]
        public void ItReturnsRandomNextGreaterThanMinLessThanMax()
        {
            // Arrange
            const int min = 2;
            const int max = 4;
            var randomGenerator = new RandomGenerator();

            // Act 
            var result = randomGenerator.Next(min, max);

            // Assert
            Assert.InRange(result, min, max);
        }
    }
}
