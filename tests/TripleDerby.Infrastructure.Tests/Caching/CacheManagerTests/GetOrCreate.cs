using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using TripleDerby.Core.DTOs;
using Xunit;

namespace TripleDerby.Infrastructure.Tests.Caching.CacheManagerTests
{
    [Trait("Category", "CacheManager")]
    public class GetOrCreate : CacheManagerTestBase
    {
        private bool _methodCalled;
        private readonly string _key;
        private readonly List<FeedingsResult>? _feedingsResults;

        public GetOrCreate()
        {
            _key = "key";
            _methodCalled = false;
            _feedingsResults = new List<FeedingsResult>
            {
                new FeedingsResult
                {
                    Id = 1,
                    Name = "Feeding",
                    Description = "Feeding"
                }
            };
        }

        [Fact]
        public async Task Given_WhenCacheHit_CacheValueReturned()
        {
            // Arrange
            CacheAdapter.Setup(x => x.GetStringAsync(It.IsAny<string>()))
                .ReturnsAsync(JsonSerializer.Serialize(_feedingsResults));

            // Act 
            var result = await CacheManager.GetOrCreate(_key, async () => await DefaultMethod());

            // Assert
            Assert.False(_methodCalled);
            Assert.IsAssignableFrom<IEnumerable<FeedingsResult>>(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task Given_WhenCacheMiss_ExecutesFunction()
        {
            // Arrange
            CacheAdapter.Setup(x => x.GetStringAsync(It.IsAny<string>()))
                .ReturnsAsync(string.Empty);

            // Act 
            var result = await CacheManager.GetOrCreate(_key, async () => await DefaultMethod());

            // Assert
            Assert.True(_methodCalled);
            Assert.IsAssignableFrom<IEnumerable<FeedingsResult>>(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task Given_WhenCacheMiss_SetsCache()
        {
            // Arrange
            CacheAdapter.Setup(x => x.GetStringAsync(It.IsAny<string>()))
                .ReturnsAsync(string.Empty);

            // Act 
            await CacheManager.GetOrCreate(_key, async () => await DefaultMethod());

            // Assert
            var value = JsonSerializer.Serialize(_feedingsResults!.ToList());
            CacheAdapter.Verify(x => x.SetStringAsync(_key, value, It.IsAny<DistributedCacheEntryOptions>()));
        }

        private async Task<IEnumerable<FeedingsResult>> DefaultMethod()
        {
            _methodCalled = true;

            return await Task.FromResult(_feedingsResults.AsEnumerable());
        }
    }
}
