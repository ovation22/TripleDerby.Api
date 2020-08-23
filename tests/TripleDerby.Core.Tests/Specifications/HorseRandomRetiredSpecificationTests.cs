using System;
using System.Collections.Generic;
using System.Linq;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Specifications;
using Xunit;

namespace TripleDerby.Core.Tests.Specifications
{
    public class HorseRandomRetiredSpecificationTests
    {
        private readonly Guid _damId;
        private readonly Guid _sireId;
        private readonly Guid _notRetiredSireId;

        public HorseRandomRetiredSpecificationTests()
        {
            _damId = new Guid("F5FC31D9-2AB5-4C01-B1D9-904518279E33");
            _sireId = new Guid("5A35DA38-7D79-42C4-87E6-DD9D91A507ED");
            _notRetiredSireId = new Guid("8027D861-7532-426D-B886-06DB4038BB43");
        }

        [Fact]
        public void GivenIsMale_WhenIsRetired_ThenSireIsIncludedInResults()
        {
            // Arrange
            var spec = new HorseRandomRetiredSpecification(true);

            // Act
            var result = GetTestCollection()
                .AsQueryable()
                .Where(spec.WhereExpressions.Single());

            // Assert
            Assert.Contains(result, x => x.Id == _sireId);
        }

        [Fact]
        public void GivenIsNotMale_WhenIsRetired_ThenDamIsIncludedInResults()
        {
            // Arrange
            var spec = new HorseRandomRetiredSpecification(false);

            // Act
            var result = GetTestCollection()
                .AsQueryable()
                .Where(spec.WhereExpressions.Single());

            // Assert
            Assert.Contains(result, x => x.Id == _damId);
        }

        [Fact]
        public void GivenIsMale_WhenIsRetired_ThenNotRetiredSireIdIsNotIncludedInResults()
        {
            // Arrange
            var spec = new HorseRandomRetiredSpecification(true);

            // Act
            var result = GetTestCollection()
                .AsQueryable()
                .Where(spec.WhereExpressions.Single());

            // Assert
            Assert.DoesNotContain(result, x => x.Id == _notRetiredSireId);
        }

        private IEnumerable<Horse> GetTestCollection()
        {
            var horses = new List<Horse>
            {
                new Horse{ Id = new Guid("85A400B6-827A-414B-BCDC-115A16921888"), IsMale = false, IsRetired = true},
                new Horse{ Id = _damId, IsMale = false, IsRetired = true },
                new Horse{ Id = _sireId, IsMale = true, IsRetired = true },
                new Horse{ Id = _notRetiredSireId, IsMale = true, IsRetired = false }
            };

            return horses;
        }
    }
}
