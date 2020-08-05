using Microsoft.EntityFrameworkCore;
using TripleDerby.Infrastructure.Data;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories.TripleDerbyRepositoryTests
{
    public class TripleDerbyRepositoryTestBase
    {
        protected DbContextOptions<TripleDerbyContext> Options;

        public TripleDerbyRepositoryTestBase()
        {
            Options = new DbContextOptionsBuilder<TripleDerbyContext>()
                .UseInMemoryDatabase("TripleDerby")
                .Options;
        }
    }
}
