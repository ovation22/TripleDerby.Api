using System;
using System.Threading.Tasks;
using TripleDerby.Core.Entities;
using TripleDerby.Infrastructure.Data;
using TripleDerby.Infrastructure.Data.Repositories;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories.EFRepositoryTests
{
    [Trait("Category", "EFRepository")]
    public class Get : EFRepositoryTestBase
    {
        private readonly Guid _id;

        public Get()
        {
            _id = Guid.NewGuid();

            using var context = new TripleDerbyContext(Options);
            context.Horses.Add(new Horse {Id = _id});
            context.SaveChanges();
        }

        [Fact]
        public async Task ItReturnsHorse()
        {
            // Arrange
            await using var context = new TripleDerbyContext(Options);
            var repository = new TripleDerbyRepository(context);

            // Act
            var horse = await repository.Get<Horse>(x => x.Id == _id);

            // Assert
            Assert.IsAssignableFrom<Horse>(horse);
        }
    }
}