using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Specifications;
using Xunit;

namespace TripleDerby.Core.Tests.Services.TrainingServiceTests
{
    public class Get : TrainingServiceTestBase
    {
        private readonly byte _id;
        private readonly Training _training;

        public Get()
        {
            _id = 4;
            _training = new Training
                {
                    Id = _id
                };

            Repository.Setup(x => x.Get(It.IsAny<TrainingSpecification>())).ReturnsAsync(_training);
        }

        [Fact]
        public async Task ItReturnsTraining()
        {
            // Arrange
            // Act
            var training = await Service.Get(_id);

            // Assert
            Assert.NotNull(training);
            Assert.IsAssignableFrom<TrainingResult>(training);
        }
    }
}