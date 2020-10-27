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
    public class Races
    {
        private readonly HttpClient _client;

        public Races(ApiTestContext testContext)
        {
            _client = testContext.HttpClient;
        }

        [Fact]
        public async Task ItGetsRaces()
        {
            // Arrange
            // Act
            var response = await _client.GetAsync("/api/Races");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var viewResult = Assert.IsType<HttpResponseMessage>(response);
            var model = Assert.IsAssignableFrom<IEnumerable<RacesResult>>(JsonSerializer.Deserialize<IEnumerable<RacesResult>>(await viewResult.Content.ReadAsStringAsync()));
            Assert.NotNull(model);
        }
    }
}
