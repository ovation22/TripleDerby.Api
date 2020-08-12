using Xunit;

namespace TripleDerby.Core.Tests.Services.BreedingServiceTests
{
    public class GetRandomGender : BreedingServiceTestBase
    {
        [Fact]
        public void GivenRandomReturns1ItReturnsFalse()
        {
            // Arrange
            RandomGenerator.Setup(x => x.Next(1, 3)).Returns(1);

            // Act
            var gender = Service.GetRandomGender();

            // Assert
            Assert.False(gender);
        }

        [Fact]
        public void GivenRandomReturns2ItReturnsTrue()
        {
            // Arrange
            RandomGenerator.Setup(x => x.Next(1, 3)).Returns(2);

            // Act
            var gender = Service.GetRandomGender();

            // Assert
            Assert.True(gender);
        }

        [Fact]
        public void GivenRandomReturns3ItReturnsTrue()
        {
            // Arrange
            RandomGenerator.Setup(x => x.Next(1, 3)).Returns(3);

            // Act
            var gender = Service.GetRandomGender();

            // Assert
            Assert.True(gender);
        }
    }
}
