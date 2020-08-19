using System.Collections.Generic;
using System.Linq;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Specifications;
using Xunit;

namespace TripleDerby.Core.Tests.Specifications
{
    public class TrainingSpecificationTests
    {
        private readonly byte _id;

        public TrainingSpecificationTests()
        {
            _id = 4;
        }

        [Fact]
        public void WhenTrainingFoundWithId_ThenTrainingReturned()
        {
            // Arrange
            var spec = new TrainingSpecification(_id);

            // Act
            var result = GetTestCollection()
                .AsQueryable()
                .Single(spec.WhereExpressions.Single());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_id, result.Id);
        }

        [Fact]
        public void WhenTrainingNotFoundWithId_ThenNull()
        {
            // Arrange
            byte badTrainingId = 9;
            var spec = new TrainingSpecification(badTrainingId);

            // Act
            var result = GetTestCollection()
                .AsQueryable()
                .FirstOrDefault(spec.WhereExpressions.Single());

            // Assert
            Assert.Null(result);
        }

        private IEnumerable<Training> GetTestCollection()
        {
            var horses = new List<Training>
            {
                new Training{ Id = 11 },
                new Training{ Id = 12 }, 
                new Training{ Id = _id }, 
                new Training{ Id = 14 }
            };

            return horses;
        }
    }
}
