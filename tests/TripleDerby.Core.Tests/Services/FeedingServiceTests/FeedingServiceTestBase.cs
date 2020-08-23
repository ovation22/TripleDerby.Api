using System.Collections.Generic;
using Moq;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Interfaces.Repositories;
using TripleDerby.Core.Services;

namespace TripleDerby.Core.Tests.Services.FeedingServiceTests
{
    public class FeedingServiceTestBase
    {
        protected internal FeedingService Service;
        protected internal Mock<ITripleDerbyRepository> Repository;

        public FeedingServiceTestBase()
        {
            Repository = new Mock<ITripleDerbyRepository>();
            Repository.Setup(x => x.GetAll<Feeding>())
                .ReturnsAsync(new List<Feeding>());

            Service = new FeedingService(Repository.Object);
        }
    }
}
