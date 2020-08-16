using System;
using System.Threading.Tasks;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Specifications;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories.EFRepositoryTests
{
    [Collection("ContextFixture")]
    [Trait("Category", "EFRepository")]
    public class Get : EFRepositoryTestBase
    {
        private readonly Guid _id;

        public Get(ContextFixture fixture) : base(fixture)
        {
            _id = Guid.NewGuid();

            Context.Horses.Add(new Horse { Id = _id, Color = new Color() });
            Context.SaveChanges();
        }

        [Fact]
        public async Task ItReturnsHorse()
        {
            // Arrange
            // Act
            var horse = await Repository.Get(new HorseSpecification(_id));

            // Assert
            Assert.IsAssignableFrom<Horse>(horse);
        }
    }
}