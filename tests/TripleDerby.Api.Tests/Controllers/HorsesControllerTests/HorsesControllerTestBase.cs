using Moq;
using TripleDerby.Api.Controllers;
using TripleDerby.Core.Interfaces.Logging;
using TripleDerby.Core.Interfaces.Services;

namespace TripleDerby.Api.Tests.Controllers.HorsesControllerTests
{
    public class HorsesControllerTestBase
    {
        protected internal HorsesController Controller;
        protected internal Mock<IHorseService> HorseService;
        protected internal Mock<ILoggerAdapter<HorsesController>> Logger;

        public HorsesControllerTestBase()
        {
            HorseService = new Mock<IHorseService>();
            Logger = new Mock<ILoggerAdapter<HorsesController>>();

            Controller = new HorsesController(HorseService.Object, Logger.Object);
        }
    }
}
