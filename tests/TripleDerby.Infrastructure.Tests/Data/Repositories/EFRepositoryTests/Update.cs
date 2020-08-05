using System;
using System.Threading.Tasks;
using TripleDerby.Core.Entities;
using TripleDerby.Infrastructure.Data;
using TripleDerby.Infrastructure.Data.Repositories;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories.EFRepositoryTests
{
    [Trait("Category", "EFRepository")]
    public class Update : EFRepositoryTestBase
    {
        private readonly Horse _horse;

        public Update()
        {
            _horse = new Horse{Id = new Guid("5FD2E324-A935-484E-8F9F-F52E7921EF21")};
            using var context = new TripleDerbyContext(Options);
            context.Horses.Add(_horse);
            context.SaveChanges();
        }

        [Fact]
        public async Task ItUpdatesHorse()
        {
            // Arrange
            await using var context = new TripleDerbyContext(Options);
            var repository = new TripleDerbyRepository(context);

            _horse.Name = "Updated";
            
            // Act
            await repository.Update(_horse);

            // Assert
            Assert.Contains(context.Horses, x => x.Name == "Updated");
        }
    }
}