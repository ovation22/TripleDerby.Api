using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
        public async Task Test1()
        {
            // Arrange
            // Act
            var response = await _client.GetAsync("/api/Horses");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
