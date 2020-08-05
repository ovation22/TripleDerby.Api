using System;
using Microsoft.EntityFrameworkCore;
using TripleDerby.Infrastructure.Data;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories
{
    public class ContextFixture : IDisposable
    {
        public TripleDerbyContext Context { get; }

        public ContextFixture()
        {
            var options = new DbContextOptionsBuilder<TripleDerbyContext>()
                .UseInMemoryDatabase("TripleDerby")
                .Options;

            Context = new TripleDerbyContext(options);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
