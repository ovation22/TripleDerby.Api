using System;
using System.Collections.Generic;
using System.Linq;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Enums;
using TripleDerby.Core.Specifications;
using Xunit;

namespace TripleDerby.Core.Tests.Specifications
{
    public class ParentHorseSpecificationTests
    {
        private readonly Guid _id;

        public ParentHorseSpecificationTests()
        {
            _id = new Guid("FB9C03F4-B327-4B27-A947-4A5983C1508F");
        }

        [Fact]
        public void ReturnsParentWithColorAndStatistics()
        {
            // Arrange
            var spec = new ParentHorseSpecification(_id);

            // Act
            var result = GetTestCollection()
                .AsQueryable()
                .FirstOrDefault(spec.WhereExpressions.Single());

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Color);
            Assert.Equal(5, result.Statistics.Count);
        }

        public List<Horse> GetTestCollection()
        {
            var horses = new List<Horse>
            {
                new Horse
                {
                    Id = _id,
                    Color = new Color {Id = 1},
                    Statistics = new List<HorseStatistic>
                    {
                        new HorseStatistic {StatisticId = StatisticId.Agility},
                        new HorseStatistic {StatisticId = StatisticId.Durability},
                        new HorseStatistic {StatisticId = StatisticId.Speed},
                        new HorseStatistic {StatisticId = StatisticId.Stamina},
                        new HorseStatistic {StatisticId = StatisticId.Happiness}
                    }
                }
            };

            return horses;
        }
    }
}
