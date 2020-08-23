using Moq;
using TripleDerby.Api.Controllers;
using TripleDerby.Core.Interfaces.Logging;
using TripleDerby.Core.Interfaces.Services;

namespace TripleDerby.Api.Tests.Controllers.FeedingsControllerTests
{
    public class FeedingsControllerTestBase
    {
        protected internal FeedingsController Controller;
        protected internal Mock<IFeedingService> FeedingService;
        protected internal Mock<ILoggerAdapter<FeedingsController>> Logger;

        public FeedingsControllerTestBase()
        {
            FeedingService = new Mock<IFeedingService>();
            Logger = new Mock<ILoggerAdapter<FeedingsController>>();

            Controller = new FeedingsController(FeedingService.Object, Logger.Object);
        }
    }
}
