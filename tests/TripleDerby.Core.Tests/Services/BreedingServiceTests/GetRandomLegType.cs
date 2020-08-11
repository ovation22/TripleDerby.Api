using Moq;
using TripleDerby.Core.Enums;
using Xunit;
using Xunit.Repeat;

namespace TripleDerby.Core.Tests.Services.BreedingServiceTests
{
    public class GetRandomLegType : BreedingServiceTestBase
    {
        [Theory]
        [Repeat(10)]
        public void GivenRandomReturns1ItReturnsFrontRunner(int count)
        {
            // Arrange
            RandomGenerator.Setup(x => x.Next(1, It.IsAny<int>())).Returns(1);

            // Act
            var legTypeId = Service.GetRandomLegType();

            // Assert
            Assert.InRange(count, 0, 10);
            Assert.Equal(LegTypeId.FrontRunner, legTypeId);
        }

        [Theory]
        [Repeat(10)]
        public void GivenRandomReturns2ItReturnsFrontRunner(int count)
        {
            // Arrange
            RandomGenerator.Setup(x => x.Next(1, It.IsAny<int>())).Returns(2);

            // Act
            var legTypeId = Service.GetRandomLegType();

            // Assert
            Assert.InRange(count, 0, 10);
            Assert.Equal(LegTypeId.StartDash, legTypeId);
        }

        [Theory]
        [Repeat(10)]
        public void GivenRandomReturns3ItReturnsFrontRunner(int count)
        {
            // Arrange
            RandomGenerator.Setup(x => x.Next(1, It.IsAny<int>())).Returns(3);

            // Act
            var legTypeId = Service.GetRandomLegType();

            // Assert
            Assert.InRange(count, 0, 10);
            Assert.Equal(LegTypeId.LastSpurt, legTypeId);
        }

        [Theory]
        [Repeat(10)]
        public void GivenRandomReturns4ItReturnsFrontRunner(int count)
        {
            // Arrange
            RandomGenerator.Setup(x => x.Next(1, It.IsAny<int>())).Returns(4);

            // Act
            var legTypeId = Service.GetRandomLegType();

            // Assert
            Assert.InRange(count, 0, 10);
            Assert.Equal(LegTypeId.StretchRunner, legTypeId);
        }

        [Theory]
        [Repeat(10)]
        public void GivenRandomReturns5ItReturnsFrontRunner(int count)
        {
            // Arrange
            RandomGenerator.Setup(x => x.Next(1, It.IsAny<int>())).Returns(5);

            // Act
            var legTypeId = Service.GetRandomLegType();

            // Assert
            Assert.InRange(count, 0, 10);
            Assert.Equal(LegTypeId.RailRunner, legTypeId);
        }
    }
}
