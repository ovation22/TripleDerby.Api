using System.Collections.Generic;
using Moq;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Interfaces.Repositories;
using TripleDerby.Core.Services;
using TripleDerby.Core.Specifications;

namespace TripleDerby.Core.Tests.Services.HorseServiceTests
{
    public class HorseServiceTestBase
    {
        protected internal HorseService Service;
        protected internal Mock<ITripleDerbyRepository> Repository;

        public HorseServiceTestBase()
        {
            Repository = new Mock<ITripleDerbyRepository>();
            Repository.Setup(x => x.List(It.IsAny<HorsesPaginatedSpecification>()))
                .ReturnsAsync(new List<Horse>());

            Service = new HorseService(Repository.Object);
        }
    }
}
