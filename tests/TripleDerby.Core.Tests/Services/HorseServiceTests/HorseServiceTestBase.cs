using Moq;
using TripleDerby.Core.Interfaces.Repositories;
using TripleDerby.Core.Services;

namespace TripleDerby.Core.Tests.Services.HorseServiceTests
{
    public class HorseServiceTestBase
    {
        protected internal HorseService Service;
        protected internal Mock<ITripleDerbyRepository> Repository;

        public HorseServiceTestBase()
        {
            Repository = new Mock<ITripleDerbyRepository>();

            Service = new HorseService(Repository.Object);
        }
    }
}
