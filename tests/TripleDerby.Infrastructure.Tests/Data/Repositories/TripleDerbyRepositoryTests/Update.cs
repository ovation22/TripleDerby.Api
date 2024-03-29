﻿using System;
using TripleDerby.Core.Entities;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories.TripleDerbyRepositoryTests
{
    [Collection("ContextFixture")]
    [Trait("Category", "TripleDerbyRepository")]
    public class Update : TripleDerbyRepositoryTestBase
    {
        private readonly Horse _horse;

        public Update(ContextFixture fixture) : base(fixture)
        {
            _horse = new Horse{ Id = new Guid("5FD2E324-A935-484E-8F9F-F52E7921EF21"), Name = "horse" };
            Context.Horses.Add(_horse);
            Context.SaveChanges();
        }

        [Fact]
        public void ItUpdatesHorse()
        {
            // Arrange
            _horse.Name = "Updated";
            
            // Act
            Repository.Update(_horse);

            // Assert
            Assert.Contains(Context.Horses, x => x.Name == "Updated");
        }
    }
}