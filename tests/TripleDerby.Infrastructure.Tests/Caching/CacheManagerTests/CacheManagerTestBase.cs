using Microsoft.Extensions.Options;
using Moq;
using TripleDerby.Core.Cache;
using TripleDerby.Core.Interfaces.Caching;
using TripleDerby.Infrastructure.Caching;

namespace TripleDerby.Infrastructure.Tests.Caching.CacheManagerTests
{
    public class CacheManagerTestBase
    {
        protected readonly CacheManager CacheManager;
        protected readonly Mock<IOptions<CacheConfig>> CacheOptions;
        protected internal Mock<IDistributedCacheAdapter> CacheAdapter;

        public CacheManagerTestBase()
        {
            CacheOptions = new Mock<IOptions<CacheConfig>>();
            CacheAdapter = new Mock<IDistributedCacheAdapter>();

            CacheOptions.Setup(x => x.Value).Returns(() => new CacheConfig { DefaultExpirationMinutes = 2 });

            CacheManager = new CacheManager(CacheAdapter.Object, CacheOptions.Object);
        }
    }
}
