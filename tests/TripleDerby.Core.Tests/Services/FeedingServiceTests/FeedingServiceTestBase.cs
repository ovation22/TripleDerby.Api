using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.DTOs;
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
        protected internal Mock<ICacheManager> Cache;
        protected internal Mock<IRandomGenerator> RandomGenerator;
        protected internal Mock<ITripleDerbyRepository> Repository;

        public FeedingServiceTestBase()
        {
            Cache = new Mock<ICacheManager>();
            RandomGenerator = new Mock<IRandomGenerator>();
            Repository = new Mock<ITripleDerbyRepository>();
            
            Repository.Setup(x => x.GetAll<Feeding>()).ReturnsAsync(new List<Feeding>());

            Cache.Setup(x => x.GetOrCreate(It.IsAny<string>(), It.IsAny<Func<Task<IEnumerable<FeedingsResult>>>>()));

            Service = new FeedingService(Cache.Object, RandomGenerator.Object, Repository.Object);
        }
    }
}
