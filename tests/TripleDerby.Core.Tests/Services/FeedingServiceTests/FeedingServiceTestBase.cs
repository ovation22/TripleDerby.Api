using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Moq;
using TripleDerby.Core.Cache;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Interfaces.Caching;
using TripleDerby.Core.Interfaces.Repositories;
using TripleDerby.Core.Interfaces.Utilities;
using TripleDerby.Core.Services;

namespace TripleDerby.Core.Tests.Services.FeedingServiceTests
{
    public class FeedingServiceTestBase
    {
        protected internal FeedingService Service;
        protected internal Mock<IDistributedCacheAdapter> Cache;
        protected internal Mock<IRandomGenerator> RandomGenerator;
        protected internal Mock<ITripleDerbyRepository> Repository;
        protected readonly Mock<IOptions<CacheConfig>> CacheOptions;

        public FeedingServiceTestBase()
        {
            Cache = new Mock<IDistributedCacheAdapter>();
            RandomGenerator = new Mock<IRandomGenerator>();
            CacheOptions = new Mock<IOptions<CacheConfig>>();
            Repository = new Mock<ITripleDerbyRepository>();
            
            Repository.Setup(x => x.GetAll<Feeding>()).ReturnsAsync(new List<Feeding>());

            CacheOptions.Setup(x => x.Value).Returns(() => new CacheConfig { DefaultExpirationMinutes = 2 });

            Service = new FeedingService(Cache.Object, RandomGenerator.Object, Repository.Object, CacheOptions.Object);
        }
    }
}
