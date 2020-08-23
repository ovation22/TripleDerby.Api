using System;
using System.Collections.Generic;
using System.Linq;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Specifications;
using Xunit;

namespace TripleDerby.Core.Tests.Specifications
{
    public class HorseRandomRacerSpecificationTests
    {
        private readonly Guid _retiredHorseId;
        private readonly Guid _notRetiredHorseId;

        public HorseRandomRacerSpecificationTests()
        {
            _retiredHorseId = new Guid("44FD32B4-009A-4BD4-9E5E-A23C4E45D6C2");
            _notRetiredHorseId = new Guid("0B557A2A-AEC7-431C-BF5A-968D273A06C3");
        }

        [Fact]
        public void ReturnsRacerHorse()
        {
            // Arrange
            var spec = new HorseRandomRacerSpecification();

            // Act
            var result = GetTestCollection()
                .AsQueryable()
                .Where(spec.WhereExpressions.Single());

            // Assert
            Assert.Contains(result, x => x.Id == _notRetiredHorseId);
        }

        [Fact]
        public void DoesNotReturnRetiredHorse()
        {
            // Arrange
            var spec = new HorseRandomRacerSpecification();

            // Act
            var result = GetTestCollection()
                .AsQueryable()
                .Where(spec.WhereExpressions.Single());

            // Assert
            Assert.DoesNotContain(result, x => x.Id == _retiredHorseId);
        }

        private IEnumerable<Horse> GetTestCollection()
        {
            var horses = new List<Horse>
            {
                new Horse{ Id = _retiredHorseId, IsRetired = true},
                new Horse{ Id = _notRetiredHorseId, IsRetired = false}
            };

            return horses;
        }
    }
}
