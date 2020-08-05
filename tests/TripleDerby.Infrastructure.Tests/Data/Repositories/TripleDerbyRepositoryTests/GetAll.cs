using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleDerby.Core.Entities;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories.TripleDerbyRepositoryTests
{
    [Trait("Category", "TripleDerbyRepository")]
    public class GetAll : TripleDerbyRepositoryTestBase
    {
        public GetAll(ContextFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task ItReturnsAllHorseByExpression()
        {
            // Arrange
            // Act
            var horses = (List<Horse>) await Repository.GetAll<Horse>(x => true);

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Horse>>(horses);
            Assert.Equal(Context.Horses.Count(), horses.Count);
        }
    }
}