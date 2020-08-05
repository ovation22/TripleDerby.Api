using System;
using TripleDerby.Core.Entities;
using TripleDerby.Infrastructure.Data;
using TripleDerby.Infrastructure.Data.Repositories;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories.EFRepositoryTests
{
    public class EFRepositoryTestBase : IClassFixture<ContextFixture>
    {
        protected readonly TripleDerbyContext Context;
        protected readonly TripleDerbyRepository Repository;

        public EFRepositoryTestBase(ContextFixture fixture)
        {
            Context = fixture.Context;
            Context.Horses.Add(new Horse { Id = Guid.NewGuid() });
            Context.Horses.Add(new Horse { Id = Guid.NewGuid() });
            Context.Horses.Add(new Horse { Id = Guid.NewGuid() });
            Context.SaveChanges();

            Repository = new TripleDerbyRepository(Context);
        }
    }
}
