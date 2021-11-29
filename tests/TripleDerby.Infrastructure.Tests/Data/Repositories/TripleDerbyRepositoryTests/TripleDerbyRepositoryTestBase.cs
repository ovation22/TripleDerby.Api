using System;
using TripleDerby.Core.Entities;
using TripleDerby.Infrastructure.Data;
using TripleDerby.Infrastructure.Data.Repositories;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories.TripleDerbyRepositoryTests
{
    public class TripleDerbyRepositoryTestBase : IClassFixture<ContextFixture>
    {
        protected readonly TripleDerbyContext Context;
        protected readonly TripleDerbyRepository Repository;

        public TripleDerbyRepositoryTestBase(ContextFixture fixture)
        {
            Context = fixture.Context;
            Context.Horses.Add(new Horse { Id = Guid.NewGuid(), Name = "horse1" });
            Context.Horses.Add(new Horse { Id = Guid.NewGuid(), Name = "horse2" });
            Context.Horses.Add(new Horse { Id = Guid.NewGuid(), Name = "horse3" });
            Context.SaveChanges(); 

            Repository = new TripleDerbyRepository(Context);
        }
    }
}
