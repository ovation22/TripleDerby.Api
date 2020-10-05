using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Moq;
using TripleDerby.Core.DTOs;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Caching.CacheManagerTests
{
    [Trait("Category", "CacheManager")]
    public class GetOrCreate : CacheManagerTestBase
    {
        private readonly string _key;

        public GetOrCreate()
        {
            _key = "key";
        }

        [Fact]
        public async Task Given_WhenCacheHit_CacheValueReturned()
        {
            // Arrange
            var feedingsResults = new List<FeedingsResult>
            {
                new FeedingsResult
                {
                    Id = 1,
                    Name = "Feeding",
                    Description = "Feeding"
                }
            };
            CacheAdapter.Setup(x => x.GetStringAsync(It.IsAny<string>()))
                .ReturnsAsync(JsonSerializer.Serialize(feedingsResults));

            // Act 
            var result = await CacheManager.GetOrCreate(_key, async () => await GetFeedings());

            // Assert
            Assert.IsAssignableFrom<IEnumerable<FeedingsResult>>(result);
        }

        private async Task<IEnumerable<FeedingsResult>> GetFeedings()
        {
            return await Task.FromResult<IEnumerable<FeedingsResult>>(new List<FeedingsResult>());
        }
    }
}
