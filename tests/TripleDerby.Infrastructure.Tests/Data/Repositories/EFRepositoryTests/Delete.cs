using System;
using System.Threading.Tasks;
using TripleDerby.Core.Entities;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories.EFRepositoryTests
{
    [Collection("ContextFixture")]
    [Trait("Category", "EFRepository")]
    public class Delete : EFRepositoryTestBase
    {
        private readonly Horse _horse;

        public Delete(ContextFixture fixture) : base(fixture)
        {
            _horse = new Horse { Id = new Guid("D6012CB6-6184-4AB4-BE14-B29C61F2CB32") };
            Context.Horses.Add(_horse);
            Context.SaveChanges();
        }

        [Fact]
        public async Task ItRemovesHorse()
        {
            // Arrange
            // Act
            await Repository.Delete(_horse);

            // Assert
            Assert.DoesNotContain(Context.Horses, x => x == _horse);
        }
    }
}