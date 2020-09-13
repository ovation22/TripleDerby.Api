using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TripleDerby.Core.Cache;
using TripleDerby.Core.Interfaces.Caching;
using TripleDerby.Infrastructure.Caching;

namespace TripleDerby.Api.Config
{
    [ExcludeFromCodeCoverage]
    public static class CachingConfig
    {
        public static void AddCaching(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment hostContext
        )
        {
            services.AddDistributedMemoryCache();
            services.Configure<CacheConfig>(configuration.GetSection("Cache"));
            services.AddSingleton(typeof(IDistributedCacheAdapter), typeof(DistributedCacheAdapter));

            if (hostContext.IsDevelopment() || hostContext.IsEnvironment("Local"))
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = configuration["Cache:Configuration"];
                    options.InstanceName = configuration["Cache:InstanceName"];
                });
            }
        }
    }
}