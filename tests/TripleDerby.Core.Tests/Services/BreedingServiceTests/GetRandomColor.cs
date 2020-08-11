using System.Threading.Tasks;
using Moq;
using Xunit;
using Xunit.Repeat;

namespace TripleDerby.Core.Tests.Services.BreedingServiceTests
{
    public class GetRandomColor : BreedingServiceTestBase
    {
        [Theory]
        [Repeat(10)]
        public async Task ItReturnsColor(int count)
        {
            // Arrange
            const bool isDamSpecial = false;
            const bool isSireSpecial = false;
            const bool includeSpecialColors = true;
            RandomGenerator.Setup(x => x.Next(1, It.IsAny<int>())).Returns(1);

            // Act
            var color = await Service.GetRandomColor(isSireSpecial, isDamSpecial, includeSpecialColors);

            // Assert
            Assert.InRange(count, 0, 10);
            Assert.IsAssignableFrom<Entities.Color>(color);
        }
    }
}
