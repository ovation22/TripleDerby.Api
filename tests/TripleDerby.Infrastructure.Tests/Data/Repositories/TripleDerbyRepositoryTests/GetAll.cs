﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleDerby.Core.Entities;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories.TripleDerbyRepositoryTests
{
    [Collection("ContextFixture")]
    [Trait("Category", "TripleDerbyRepository")]
    public class GetAll : TripleDerbyRepositoryTestBase
    {
        public GetAll(ContextFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task ItReturnsAllHorse()
        {
            // Arrange
            // Act
            var horses = (List<Horse>)await Repository.GetAll<Horse>();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Horse>>(horses);
            Assert.Equal(Context.Horses.Count(), horses.Count);
        }
    }
}