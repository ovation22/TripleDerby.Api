using System.Collections.Generic;
using Moq;
using TripleDerby.Core.Entities;
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
            Repository.Setup(x => x.GetAll<Race>())
                .ReturnsAsync(new List<Race>());

            Service = new RaceService(Repository.Object);
        }
    }
}
