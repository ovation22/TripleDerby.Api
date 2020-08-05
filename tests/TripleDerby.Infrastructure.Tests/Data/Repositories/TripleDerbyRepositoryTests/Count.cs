using System;
using System.Linq;
using System.Threading.Tasks;
using TripleDerby.Core.Entities;
using TripleDerby.Infrastructure.Data;
using TripleDerby.Infrastructure.Data.Repositories;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories.TripleDerbyRepositoryTests
{
    [Trait("Category", "TripleDerbyRepository")]
    public class Count : TripleDerbyRepositoryTestBase
    {
        public Count()
        {
            using var context = new TripleDerbyContext(Options);
            context.Horses.Add(new Horse());
            context.SaveChanges();
        }

        [Fact]
        public async Task ItReturnsHorse()
        {
            // Arrange
            await using var context = new TripleDerbyContext(Options);
            var repository = new TripleDerbyRepository(context);

            // Act
            var count = await repository.Count<Horse>();

            // Assert
            Assert.Equal(context.Horses.Count(), count);
        }
    }
}