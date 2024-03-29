﻿using System;
using System.Threading.Tasks;
using TripleDerby.Core.Entities;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories.TripleDerbyRepositoryTests
{
    [Collection("ContextFixture")]
    [Trait("Category", "TripleDerbyRepository")]
    public class Delete : TripleDerbyRepositoryTestBase
    {
        private readonly Horse _horse;

        public Delete(ContextFixture fixture) : base(fixture)
        {
            _horse = new Horse { Id = new Guid("D6012CB6-6184-4AB4-BE14-B29C61F2CB32"), Name = "delete me" };
            Context.Horses.Add(_horse);
            Context.SaveChanges();
        }

        [Fact]
        public async Task ItRemovesHorse()
        {
            // Arrange
            // Act
            Repository.Delete(_horse);
            await Repository.Save();

            // Assert
            Assert.DoesNotContain(Context.Horses, x => x == _horse);
        }
    }
}