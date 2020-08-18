using Moq;
using TripleDerby.Api.Controllers;
using TripleDerby.Core.Interfaces.Logging;
using TripleDerby.Core.Interfaces.Services;

namespace TripleDerby.Api.Tests.Controllers.RacesControllerTests
{
    public class RacesControllerTestBase
    {
        protected internal RacesController Controller;
        protected internal Mock<IRaceService> RaceService;
        protected internal Mock<ILoggerAdapter<RacesController>> Logger;

        public RacesControllerTestBase()
        {
            RaceService = new Mock<IRaceService>();
            Logger = new Mock<ILoggerAdapter<RacesController>>();

            Controller = new RacesController(RaceService.Object, Logger.Object);
        }
    }
}
