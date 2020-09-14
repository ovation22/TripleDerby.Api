using System.Collections.Generic;
using System.Threading.Tasks;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Specifications;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories.TripleDerbyRepositoryTests
{
    [Collection("ContextFixture")]
    [Trait("Category", "TripleDerbyRepository")]
    public class List : TripleDerbyRepositoryTestBase
    {
        public List(ContextFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task ItReturnsHorses()
        {
            // Arrange
            var spec = new HorsesPaginatedSpecification(1,1);

            // Act
            var horses = (List<Horse>)await Repository.List(spec);

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Horse>>(horses);
        }
    }
}