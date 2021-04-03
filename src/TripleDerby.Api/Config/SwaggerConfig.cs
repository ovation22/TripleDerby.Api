using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TripleDerby.Api.Filters;

namespace TripleDerby.Api.Config
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TripleDerby",
                    Description = "A RESTful API for TripleDerby",
                });

                c.DocumentFilter<JsonPatchDocumentFilter>();
            });
        }
        public static void UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TripleDerby API V1");
            });
        }
    }
}