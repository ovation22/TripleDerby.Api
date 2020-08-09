using Moq;
using TripleDerby.Api.Controllers;
using TripleDerby.Core.Interfaces.Logging;
using TripleDerby.Core.Interfaces.Services;

namespace TripleDerby.Api.Tests.Controllers.BreedingControllerTests
{
    public class BreedingControllerTestBase
    {
        protected internal BreedingController Controller;
        protected internal Mock<IBreedingService> BreedingService;
        protected internal Mock<ILoggerAdapter<BreedingController>> Logger;

        public BreedingControllerTestBase()
        {
            BreedingService = new Mock<IBreedingService>();
            Logger = new Mock<ILoggerAdapter<BreedingController>>();

            Controller = new BreedingController(BreedingService.Object, Logger.Object);
        }
    }
}
