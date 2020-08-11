using Xunit;
using Xunit.Repeat;

namespace TripleDerby.Core.Tests.Services.BreedingServiceTests
{
    public class GetRandomGender : BreedingServiceTestBase
    {
        [Theory]
        [Repeat(10)]
        public void GivenRandomReturns1ItReturnsFalse(int count)
        {
            // Arrange
            RandomGenerator.Setup(x => x.Next(1, 3)).Returns(1);

            // Act
            var gender = Service.GetRandomGender();

            // Assert
            Assert.InRange(count, 0, 10);
            Assert.False(gender);
        }

        [Theory]
        [Repeat(10)]
        public void GivenRandomReturns2ItReturnsTrue(int count)
        {
            // Arrange
            RandomGenerator.Setup(x => x.Next(1, 3)).Returns(2);

            // Act
            var gender = Service.GetRandomGender();

            // Assert
            Assert.InRange(count, 0, 10);
            Assert.True(gender);
        }

        [Theory]
        [Repeat(10)]
        public void GivenRandomReturns3ItReturnsTrue(int count)
        {
            // Arrange
            RandomGenerator.Setup(x => x.Next(1, 3)).Returns(3);

            // Act
            var gender = Service.GetRandomGender();

            // Assert
            Assert.InRange(count, 0, 10);
            Assert.True(gender);
        }
    }
}
