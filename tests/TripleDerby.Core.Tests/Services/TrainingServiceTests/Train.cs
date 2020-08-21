using System;
using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using Xunit;

namespace TripleDerby.Core.Tests.Services.TrainingServiceTests
{
    public class Train : TrainingServiceTestBase
    {
        private readonly Guid _horseId;
        private readonly byte _trainingId;
        private readonly TrainingSession _trainingSession = default!;

        public Train()
        {
            _trainingId = 9;
            _horseId = new Guid("B703FC6F-B72D-4048-9FEA-5264A50F8363");

            Repository.Setup(x => x.Add(It.IsAny<TrainingSession>())).ReturnsAsync(_trainingSession);
        }

        [Fact]
        public async Task ItReturnsTrainingSessionResult()
        {
            // Arrange
            // Act
            var result = await Service.Train(_trainingId, _horseId);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<TrainingSessionResult>(result);
        }
    }
}