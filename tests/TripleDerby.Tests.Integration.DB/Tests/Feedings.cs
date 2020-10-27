using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TripleDerby.Core.DTOs;
using Xunit;

namespace TripleDerby.Tests.Integration.DB.Tests
{
    [Collection(ApiTestCollection.CollectionName)]
    public class Feedings
    {
        private readonly HttpClient _client;

        public Feedings(ApiTestContext testContext)
        {
            _client = testContext.HttpClient;
        }

        [Fact]
        public async Task ItGetsRaces()
        {
            // Arrange
            // Act
            var response = await _client.GetAsync("/api/Feedings");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var viewResult = Assert.IsType<HttpResponseMessage>(response);
            var model = Assert.IsAssignableFrom<IEnumerable<FeedingsResult>>(JsonSerializer.Deserialize<IEnumerable<FeedingsResult>>(await viewResult.Content.ReadAsStringAsync()));
            Assert.NotNull(model);
        }
    }
}
