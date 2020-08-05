using System;
using System.Threading.Tasks;
using TripleDerby.Core.Entities;
using TripleDerby.Infrastructure.Data;
using TripleDerby.Infrastructure.Data.Repositories;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories.EFRepositoryTests
{
    [Trait("Category", "EFRepository")]
    public class Delete : EFRepositoryTestBase
    {
        private readonly Horse _horse;

        public Delete()
        {
            _horse = new Horse{Id = new Guid("D6012CB6-6184-4AB4-BE14-B29C61F2CB32")};
            using var context = new TripleDerbyContext(Options);
            context.Horses.Add(_horse);
            context.SaveChanges();
        }

        [Fact]
        public async Task ItRemovesHorse()
        {
            // Arrange
            await using var context = new TripleDerbyContext(Options);
            var repository = new TripleDerbyRepository(context);

            // Act
            await repository.Delete(_horse);

            // Assert
            Assert.DoesNotContain(context.Horses, x => x == _horse);
        }
    }
}