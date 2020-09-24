using System;
using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Enums;
using TripleDerby.Core.Specifications;
using Xunit;

namespace TripleDerby.Core.Tests.Services.FeedingServiceTests
{
    public class Feed : FeedingServiceTestBase
    {
        private readonly Guid _horseId;
        private readonly byte _feedingId;
        private readonly HorseStatistic _stat; 
        private readonly byte _originalHappiness;
        private readonly FeedingSession _feedingSession = default!;

        public Feed()
        {
            _feedingId = 9;
            _horseId = new Guid("A0ADF3F9-DD4B-41D1-ABB1-B2030CC5653B");
            _originalHappiness = 6;
            _stat = new HorseStatistic
            {
                HorseId = _horseId,
                Actual = _originalHappiness,
                DominantPotential = 99
            };
            Repository.Setup(x =>
                    x.Get(It.IsAny<HorseStatisticsSpecification>()))
                .ReturnsAsync(() => _stat);

            Repository.Setup(x => x.Add(It.IsAny<FeedingSession>())).Returns(_feedingSession);
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

        [Fact]
        public async Task Given_WhenFeed_ThenHappinessIncreasedByRandom()
        {
            // Arrange
            const byte random = 9;
            this.RandomGenerator.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(random);

            // Act
            await Service.Feed(_feedingId, _horseId);

            // Assert
            Assert.Equal(_originalHappiness + random, _stat.Actual);
        }

        [Fact]
        public async Task GivenActualPlusRandomExceedsDominantPotential_WhenFeed_ThenHappinessToDominant()
        {
            // Arrange
            _stat.DominantPotential = (byte) (_originalHappiness + 1);
            const byte random = 9;
            this.RandomGenerator.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(random);

            // Act
            await Service.Feed(_feedingId, _horseId);

            // Assert
            Assert.Equal(_stat.DominantPotential, _stat.Actual);
        }

        [Fact]
        public async Task Given_WhenFeedAccepted_ThenAcceptedResult()
        {
            // Arrange
            // Act
            var result = await Service.Feed(_feedingId, _horseId);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<FeedingSessionResult>(result);
            Assert.Equal(FeedResponse.Accepted, result.Result);
        }

        [Fact]
        public async Task Given_WhenFeedAccepted_ThenHappinessIncreaseByRange()
        {
            // Arrange
            this.RandomGenerator.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>()));

            // Act
            var result = await Service.Feed(_feedingId, _horseId);

            // Assert
            this.RandomGenerator.Verify(x => x.Next(0, 1));
        }

        [Fact(Skip="for now")]
        public async Task Given_WhenFeedFavorite_ThenHappinessIncreaseByRange()
        {
            // Arrange
            this.RandomGenerator.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>()));

            // Act
            var result = await Service.Feed(_feedingId, _horseId);

            // Assert
            this.RandomGenerator.Verify(x => x.Next(0, 2));
        }
    }
}