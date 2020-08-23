using System;
using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using Xunit;

namespace TripleDerby.Core.Tests.Services.FeedingServiceTests
{
    public class Feed : FeedingServiceTestBase
    {
        private readonly Guid _horseId;
        private readonly byte _feedingId;
        private readonly FeedingSession _feedingSession = default!;

        public Feed()
        {
            _feedingId = 9;
            _horseId = new Guid("A0ADF3F9-DD4B-41D1-ABB1-B2030CC5653B");

            Repository.Setup(x => x.Add(It.IsAny<FeedingSession>())).ReturnsAsync(_feedingSession);
        }

        [Fact]
        public async Task ItReturnsFeedingSessionResult()
        {
            // Arrange
            // Act
            var result = await Service.Feed(_feedingId, _horseId);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<FeedingSessionResult>(result);
        }
    }
}