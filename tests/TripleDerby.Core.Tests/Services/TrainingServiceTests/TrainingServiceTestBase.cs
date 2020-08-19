using System.Collections.Generic;
using Moq;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Interfaces.Repositories;
using TripleDerby.Core.Services;

namespace TripleDerby.Core.Tests.Services.TrainingServiceTests
{
    public class TrainingServiceTestBase
    {
        protected internal TrainingService Service;
        protected internal Mock<ITripleDerbyRepository> Repository;

        public TrainingServiceTestBase()
        {
            Repository = new Mock<ITripleDerbyRepository>();
            Repository.Setup(x => x.GetAll<Training>())
                .ReturnsAsync(new List<Training>());

            Service = new TrainingService(Repository.Object);
        }
    }
}
