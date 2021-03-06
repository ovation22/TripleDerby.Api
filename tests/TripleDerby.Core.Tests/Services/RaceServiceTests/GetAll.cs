﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Specifications;
using Xunit;

namespace TripleDerby.Core.Tests.Services.RaceServiceTests
{
    public class GetAll : RaceServiceTestBase
    {
        [Fact]
        public async Task ItReturnsRaces()
        {
            // Arrange
            // Act
            var races = await Service.GetAll();

            // Assert
            Assert.NotNull(races);
            Assert.IsAssignableFrom<IEnumerable<RacesResult>>(races);
        }

        [Fact]
        public async Task ItCallsRepository()
        {
            // Arrange
            // Act
            await Service.GetAll();

            // Assert
            Repository.Verify(x => x.List(It.IsAny<RaceSpecification>()), Times.Once());
        }
    }
}
