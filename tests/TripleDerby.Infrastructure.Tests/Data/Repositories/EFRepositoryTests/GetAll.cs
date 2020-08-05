using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleDerby.Core.Entities;
using TripleDerby.Infrastructure.Data;
using TripleDerby.Infrastructure.Data.Repositories;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories.EFRepositoryTests
{
    [Trait("Category", "EFRepository")]
    public class GetAll : EFRepositoryTestBase
    {
        public GetAll()
        {
            using var context = new TripleDerbyContext(Options);
            context.Horses.Add(new Horse {Id = Guid.NewGuid()});
            context.Horses.Add(new Horse {Id = Guid.NewGuid()});
            context.Horses.Add(new Horse {Id = Guid.NewGuid()});
            context.SaveChanges();
        }

        [Fact]
        public async Task ItReturnsAllHorse()
        {
            // Arrange
            await using var context = new TripleDerbyContext(Options);
            var repository = new TripleDerbyRepository(context);

            // Act
            var horses = (List<Horse>) await repository.GetAll<Horse>();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Horse>>(horses);
            Assert.Equal(context.Horses.Count(), horses.Count);
        }

        [Fact]
        public async Task ItReturnsAllHorseByExpression()
        {
            // Arrange
            await using var context = new TripleDerbyContext(Options);
            var repository = new TripleDerbyRepository(context);

            // Act
            var horses = (List<Horse>) await repository.GetAll<Horse>(x => true);

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Horse>>(horses);
            Assert.Equal(context.Horses.Count(), horses.Count);
        }
    }
}