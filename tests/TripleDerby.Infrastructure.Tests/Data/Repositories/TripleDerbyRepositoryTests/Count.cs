using System.Linq;
using System.Threading.Tasks;
using TripleDerby.Core.Entities;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories.TripleDerbyRepositoryTests
{
    [Collection("ContextFixture")]
    [Trait("Category", "TripleDerbyRepository")]
    public class Count : TripleDerbyRepositoryTestBase
    {
        public Count(ContextFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task ItReturnsCount()
        {
            // Arrange
            // Act
            var count = await Repository.Count<Horse>();

            // Assert
            Assert.Equal(Context.Horses.Count(), count);
        }
    }
}