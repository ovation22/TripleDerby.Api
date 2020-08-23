using System.Collections.Generic;
using System.Linq;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Specifications;
using Xunit;

namespace TripleDerby.Core.Tests.Specifications
{
    public class FeedingSpecificationTests
    {
        private readonly byte _id;

        public FeedingSpecificationTests()
        {
            _id = 4;
        }

        [Fact]
        public void WhenFeedingFoundWithId_ThenFeedingReturned()
        {
            // Arrange
            var spec = new FeedingSpecification(_id);

            // Act
            var result = GetTestCollection()
                .AsQueryable()
                .Single(spec.WhereExpressions.Single());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_id, result.Id);
        }

        [Fact]
        public void WhenFeedingNotFoundWithId_ThenNull()
        {
            // Arrange
            byte badFeedingId = 9;
            var spec = new FeedingSpecification(badFeedingId);

            // Act
            var result = GetTestCollection()
                .AsQueryable()
                .FirstOrDefault(spec.WhereExpressions.Single());

            // Assert
            Assert.Null(result);
        }

        private IEnumerable<Feeding> GetTestCollection()
        {
            var horses = new List<Feeding>
            {
                new Feeding{ Id = 11 },
                new Feeding{ Id = 12 }, 
                new Feeding{ Id = _id }, 
                new Feeding{ Id = 14 }
            };

            return horses;
        }
    }
}
