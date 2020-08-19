using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using Xunit;

namespace TripleDerby.Core.Tests.Services.TrainingServiceTests
{
    public class GetAll : TrainingServiceTestBase
    {
        [Fact]
        public async Task ItReturnsTrainings()
        {
            // Arrange
            // Act
            var trainings = await Service.GetAll();

            // Assert
            Assert.NotNull(trainings);
            Assert.IsAssignableFrom<IEnumerable<TrainingsResult>>(trainings);
        }

        [Fact]
        public async Task ItCallsRepository()
        {
            // Arrange
            // Act
            await Service.GetAll();

            // Assert
            Repository.Verify(x => x.GetAll<Training>(), Times.Once());
        }
    }
}
