using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TripleDerby.Core.DTOs;
using Xunit;

namespace TripleDerby.Tests.Integration.DB.Tests
{
    [Collection(ApiTestCollection.CollectionName)]
    public class Horses
    {
        private readonly HttpClient _client;

        public Horses(ApiTestContext testContext)
        {
            _client = testContext.HttpClient;
        }

        [Fact]
        public async Task ItGetsHorses()
        {
            // Arrange
            // Act
            var response = await _client.GetAsync("/api/Horses");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var viewResult = Assert.IsType<HttpResponseMessage>(response);
            var model = Assert.IsAssignableFrom<HorsesResult>(JsonSerializer.Deserialize<HorsesResult>(await viewResult.Content.ReadAsStringAsync()));
            Assert.NotNull(model);
        }
    }
}
