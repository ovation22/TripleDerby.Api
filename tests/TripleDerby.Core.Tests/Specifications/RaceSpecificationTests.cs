using System.Collections.Generic;
using System.Linq;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Specifications;
using Xunit;

namespace TripleDerby.Core.Tests.Specifications
{
    public class RaceSpecificationTests
    {
        private readonly byte _id;

        public RaceSpecificationTests()
        {
            _id = 4;
        }

        [Fact]
        public void WhenRaceFoundWithId_ThenRaceReturned()
        {
            // Arrange
            var spec = new RaceSpecification(_id);

            // Act
            var result = GetTestCollection()
                .AsQueryable()
                .Single(spec.WhereExpressions.Single());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_id, result.Id);
        }

        [Fact]
        public void WhenRaceNotFoundWithId_ThenNull()
        {
            // Arrange
            byte badRaceId = 9;
            var spec = new RaceSpecification(badRaceId);

            // Act
            var result = GetTestCollection()
                .AsQueryable()
                .FirstOrDefault(spec.WhereExpressions.Single());

            // Assert
            Assert.Null(result);
        }

        private IEnumerable<Race> GetTestCollection()
        {
            var horses = new List<Race>
            {
                new Race{ Id = 11 },
                new Race{ Id = 12 }, 
                new Race{ Id = _id }, 
                new Race{ Id = 14 }
            };

            return horses;
        }
    }
}
