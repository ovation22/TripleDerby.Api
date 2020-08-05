using Microsoft.EntityFrameworkCore;
using TripleDerby.Infrastructure.Data;

namespace TripleDerby.Infrastructure.Tests.Data.Repositories.EFRepositoryTests
{
    public class EFRepositoryTestBase
    {
        protected DbContextOptions<TripleDerbyContext> Options;

        public EFRepositoryTestBase()
        {
            Options = new DbContextOptionsBuilder<TripleDerbyContext>()
                .UseInMemoryDatabase("TripleDerby")
                .Options;
        }
    }
}
