using Microsoft.Extensions.Options;
using Moq;
using TripleDerby.Core.Cache;
using TripleDerby.Core.Interfaces.Caching;
using TripleDerby.Core.Interfaces.Repositories;
using TripleDerby.Core.Services;

namespace TripleDerby.Core.Tests.Services.BreedingServiceTests
{
    public class BreedingServiceTestBase
    {
        protected internal BreedingService Service;
        protected internal Mock<IDistributedCacheAdapter> Cache;
        protected internal Mock<ITripleDerbyRepository> Repository;
        protected readonly Mock<IOptions<CacheConfig>> CacheOptions;

        public BreedingServiceTestBase()
        {
            Cache = new Mock<IDistributedCacheAdapter>();
            Repository = new Mock<ITripleDerbyRepository>();
            CacheOptions = new Mock<IOptions<CacheConfig>>();

            CacheOptions.Setup(x => x.Value).Returns(() => new CacheConfig { DefaultExpirationMinutes = 2 });

            Service = new BreedingService(Cache.Object, Repository.Object, CacheOptions.Object);
        }
    }
}
