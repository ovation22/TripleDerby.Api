using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripleDerby.Infrastructure.Data;

namespace TripleDerby.Api.Config
{
    [ExcludeFromCodeCoverage]
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<TripleDerbyContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TripleDerby")));
        }
    }
}