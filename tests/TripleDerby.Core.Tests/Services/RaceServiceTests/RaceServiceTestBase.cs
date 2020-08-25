using System.Collections.Generic;
using Ardalis.Specification;
using Moq;
using TripleDerby.Core.Interfaces.Repositories;
using TripleDerby.Core.Services;

namespace TripleDerby.Core.Tests.Services.RaceServiceTests
{
    public class RaceServiceTestBase
    {
        protected internal RaceService Service;
        protected internal Mock<ITripleDerbyRepository> Repository;

        public RaceServiceTestBase()
        {
            Repository = new Mock<ITripleDerbyRepository>();
            Repository.Setup(x => x.List(It.IsAny<ISpecification<Entities.Race>>()))
                .ReturnsAsync(new List<Entities.Race>());

            Service = new RaceService(Repository.Object);
        }
    }
}
