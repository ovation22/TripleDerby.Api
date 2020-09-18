using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TripleDerby.Api;
using Xunit;

namespace TripleDerby.Integration.Tests
{
    public class FeedingTests :
        IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public FeedingTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_Feeding_ReturnsOk()
        {
            // Arrange
            var id = 1;

            //Act
            var response = await _client.GetAsync($"api/Feedings/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task WhenFeedingNotFound_ThenBadResult()
        {
            // Arrange
            var id = 99;

            //Act
            var response = await _client.GetAsync($"api/Feedings/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}