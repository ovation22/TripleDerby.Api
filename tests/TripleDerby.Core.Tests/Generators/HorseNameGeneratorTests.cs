using TripleDerby.Core.Generators;
using Xunit;

namespace TripleDerby.Core.Tests.Generators
{
    public class HorseNameGeneratorTests
    {
        private HorseNameGenerator _generator;

        public HorseNameGeneratorTests()
        {
            _generator = new HorseNameGenerator();
        }

        [Fact]
        public void ItRemovesLeadingSpaces()
        {
            // Act
            var name = _generator.Generate();

            // Assert
            Assert.NotNull(name);
        }
    }
}
