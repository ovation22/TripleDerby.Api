using Moq;
using TripleDerby.Api.Controllers;
using TripleDerby.Core.Interfaces.Logging;
using TripleDerby.Core.Interfaces.Services;

namespace TripleDerby.Api.Tests.Controllers.TrainingsControllerTests
{
    public class TrainingsControllerTestBase
    {
        protected internal TrainingsController Controller;
        protected internal Mock<ITrainingService> TrainingService;
        protected internal Mock<ILoggerAdapter<TrainingsController>> Logger;

        public TrainingsControllerTestBase()
        {
            TrainingService = new Mock<ITrainingService>();
            Logger = new Mock<ILoggerAdapter<TrainingsController>>();

            Controller = new TrainingsController(TrainingService.Object, Logger.Object);
        }
    }
}
