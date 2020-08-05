using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TripleDerby.Api.Config
{
    [ExcludeFromCodeCoverage]
    public static class HealthConfig
    {
        public static void AddHealthCheckConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("HorseMeet"))
                .AddRedis(configuration["Cache:Configuration"]);
        }

        public static void UseHealthCheckConfig(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health");
        }
    }
}