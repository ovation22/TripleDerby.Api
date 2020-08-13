using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.Entities;
using Xunit;

namespace TripleDerby.Core.Tests.Services.BreedingServiceTests
{
    public class GetRandomColor : BreedingServiceTestBase
    {
        private readonly IEnumerable<Color> _colors;

        public GetRandomColor()
        {
            _colors = GenerateTestColors();

            Repository.Setup(x => x.GetAll<Color>())
                .ReturnsAsync(_colors);
        }

        private IEnumerable<Color> GenerateTestColors()
        {
            return new List<Color>{
                new Color {Id = 1, Name = "One", Weight = 1, IsSpecial = false},
                new Color {Id = 2, Name = "Two", Weight = 2, IsSpecial = false},
                new Color {Id = 7, Name = "Five", Weight = 5, IsSpecial = false},
                new Color {Id = 8, Name = "Fifty", Weight = 50, IsSpecial = false},
                new Color {Id = 9, Name = "Hundred", Weight = 100, IsSpecial = false},
                new Color {Id = 10, Name = "One Special", Weight = 500, IsSpecial = true},
                new Color {Id = 11, Name = "Two Special", Weight = 1000, IsSpecial = true},
                new Color {Id = 12, Name = "Three Special", Weight = 2000, IsSpecial = true},
                new Color {Id = 20, Name = "Last Special", Weight = 200000, IsSpecial = true}
            };
        }

        [Fact]
        public async Task ItReturnsColor()
        {
            // Arrange
            const bool isDamSpecial = false;
            const bool isSireSpecial = false;
            const bool includeSpecialColors = false;
            RandomGenerator.Setup(x => x.Next(1, It.IsAny<int>())).Returns(1);

            // Act
            var color = await Service.GetRandomColor(isSireSpecial, isDamSpecial, includeSpecialColors);

            // Assert
            Assert.IsAssignableFrom<Color>(color);
        }

        [Fact]
        public async Task GivenSpecialColorsNotIncluded_ThenColorIsNotSpecial()
        {
            // Arrange
            const bool isDamSpecial = false;
            const bool isSireSpecial = false;
            const bool includeSpecialColors = false;

            // Act
            var color = await Service.GetRandomColor(isSireSpecial, isDamSpecial, includeSpecialColors);

            // Assert
            Assert.False(color.IsSpecial);
        }

        [Fact]
        public async Task GivenSpecialColorsNotIncluded_ThenMultiplierIsOne()
        {
            // Arrange
            const bool isDamSpecial = false;
            const bool isSireSpecial = false;
            const bool includeSpecialColors = false;
            int random = 0;
            RandomGenerator.Setup(x => x.Next(It.IsAny<int>()))
                .Callback<int>(x => random = x);

            // Act
            var color = await Service.GetRandomColor(isSireSpecial, isDamSpecial, includeSpecialColors);

            // Assert
            Assert.False(color.IsSpecial);
            Assert.Equal(_colors.Count() * _colors.Last(x => !x.IsSpecial).Weight, random);
        }

        [Fact]
        public async Task GivenSpecialColorsIsIncluded_WhenSireIsSpecial_ThenMultiplierIsTen()
        {
            // Arrange
            const int multiplier = 10;
            const bool isDamSpecial = false;
            const bool isSireSpecial = true;
            const bool includeSpecialColors = true;
            int random = 0;
            RandomGenerator.Setup(x => x.Next(It.IsAny<int>()))
                .Callback<int>(x => random = x);

            // Act
            await Service.GetRandomColor(isSireSpecial, isDamSpecial, includeSpecialColors);

            // Assert
            Assert.Equal(_colors.Count() * (_colors.Last().Weight / multiplier), random);
        }

        [Fact]
        public async Task GivenSpecialColorsIsIncluded_WhenDamIsSpecial_ThenMultiplierIsTwentyFive()
        {
            // Arrange
            const int multiplier = 25;
            const bool isDamSpecial = true;
            const bool isSireSpecial = false;
            const bool includeSpecialColors = true;
            int random = 0;
            RandomGenerator.Setup(x => x.Next(It.IsAny<int>()))
                .Callback<int>(x => random = x);

            // Act
            await Service.GetRandomColor(isSireSpecial, isDamSpecial, includeSpecialColors);

            // Assert
            Assert.Equal(_colors.Count() * (_colors.Last().Weight / multiplier), random);
        }

        [Fact]
        public async Task GivenSpecialColorsIsIncluded_WhenBothParentsAreSpecial_ThenMultiplierIsFifty()
        {
            // Arrange
            const int multiplier = 50;
            const bool isDamSpecial = true;
            const bool isSireSpecial = true;
            const bool includeSpecialColors = true;
            int random = 0;
            RandomGenerator.Setup(x => x.Next(It.IsAny<int>()))
                .Callback<int>(x => random = x);

            // Act
            await Service.GetRandomColor(isSireSpecial, isDamSpecial, includeSpecialColors);

            // Assert
            Assert.Equal(_colors.Count() * (_colors.Last().Weight / multiplier), random);
        }
    }
}
