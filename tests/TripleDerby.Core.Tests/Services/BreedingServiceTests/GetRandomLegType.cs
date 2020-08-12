using Moq;
using TripleDerby.Core.Enums;
using Xunit;

namespace TripleDerby.Core.Tests.Services.BreedingServiceTests
{
    public class GetRandomLegType : BreedingServiceTestBase
    {
        [Fact]
        public void GivenRandomReturns1ItReturnsFrontRunner()
        {
            // Arrange
            RandomGenerator.Setup(x => x.Next(1, It.IsAny<int>())).Returns(1);

            // Act
            var legTypeId = Service.GetRandomLegType();

            // Assert
            Assert.Equal(LegTypeId.FrontRunner, legTypeId);
        }

        [Fact]
        public void GivenRandomReturns2ItReturnsFrontRunner()
        {
            // Arrange
            RandomGenerator.Setup(x => x.Next(1, It.IsAny<int>())).Returns(2);

            // Act
            var legTypeId = Service.GetRandomLegType();

            // Assert
            Assert.Equal(LegTypeId.StartDash, legTypeId);
        }

        [Fact]
        public void GivenRandomReturns3ItReturnsFrontRunner()
        {
            // Arrange
            RandomGenerator.Setup(x => x.Next(1, It.IsAny<int>())).Returns(3);

            // Act
            var legTypeId = Service.GetRandomLegType();

            // Assert
            Assert.Equal(LegTypeId.LastSpurt, legTypeId);
        }

        [Fact]
        public void GivenRandomReturns4ItReturnsFrontRunner()
        {
            // Arrange
            RandomGenerator.Setup(x => x.Next(1, It.IsAny<int>())).Returns(4);

            // Act
            var legTypeId = Service.GetRandomLegType();

            // Assert
            Assert.Equal(LegTypeId.StretchRunner, legTypeId);
        }

        [Fact]
        public void GivenRandomReturns5ItReturnsFrontRunner()
        {
            // Arrange
            RandomGenerator.Setup(x => x.Next(1, It.IsAny<int>())).Returns(5);

            // Act
            var legTypeId = Service.GetRandomLegType();

            // Assert
            Assert.Equal(LegTypeId.RailRunner, legTypeId);
        }
    }
}
