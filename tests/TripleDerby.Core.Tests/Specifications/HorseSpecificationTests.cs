using System;
using System.Collections.Generic;
using System.Linq;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Specifications;
using Xunit;

namespace TripleDerby.Core.Tests.Specifications
{
    public class HorseSpecificationTests
    {
        private readonly Guid _id;

        public HorseSpecificationTests()
        {
            _id = new Guid("BB2316DE-9CF6-4F17-BF72-2B77C7BD1E88");
        }

        [Fact]
        public void WhenHorseFoundWithId_ThenHorseReturned()
        {
            // Arrange
            var spec = new HorseSpecification(_id);

            // Act
            var result = GetTestCollection()
                .AsQueryable()
                .Single(spec.WhereExpressions.Single());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_id, result.Id);
        }

        [Fact]
        public void WhenHorseNotFoundWIthId_ThenNull()
        {
            // Arrange
            var badHorseId = new Guid("172B6257-582D-4453-A13F-41C6CBE4CAB2");
            var spec = new HorseSpecification(badHorseId);

            // Act
            var result = GetTestCollection()
                .AsQueryable()
                .FirstOrDefault(spec.WhereExpressions.Single());

            // Assert
            Assert.Null(result);
        }

        private IEnumerable<Horse> GetTestCollection()
        {
            var horses = new List<Horse>
            {
                new Horse{ Id = new Guid("D7728576-CC03-40E2-A92B-2E47FC791C60")}, 
                new Horse{ Id = new Guid("B067FA84-7940-4D6D-9170-F0EDAD986C87")}, 
                new Horse{ Id = _id}, 
                new Horse{ Id = new Guid("D6506015-E4E5-4057-9B42-A112C8B08C56") }
            };

            return horses;
        }
    }
}
