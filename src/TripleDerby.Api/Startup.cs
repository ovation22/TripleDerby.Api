using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TripleDerby.Api.Config;
using TripleDerby.Core.Interfaces.Logging;
using TripleDerby.Core.Interfaces.Repositories;
using TripleDerby.Core.Interfaces.Services;
using TripleDerby.Core.Interfaces.Utilities;
using TripleDerby.Core.Services;
using TripleDerby.Infrastructure.Data;
using TripleDerby.Infrastructure.Data.Repositories;
using TripleDerby.Infrastructure.Logging;
using TripleDerby.Infrastructure.Utilities;

namespace TripleDerby.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _hostContext;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostContext)
        {
            Configuration = configuration;
            _hostContext = hostContext;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorsConfig();
            services.AddControllersConfig();
            services.AddDatabaseConfig(Configuration);
            services.AddSwaggerConfig();
            services.AddApplicationInsightsTelemetry();

            if (_hostContext.IsProduction())
            {
                services.AddHealthCheckConfig(Configuration);
            }

            services.AddSingleton<ITimeManager, TimeManager>();
            services.AddSingleton<IRandomGenerator, RandomGenerator>();
            services.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
            services.AddScoped(typeof(ITripleDerbyRepository), typeof(TripleDerbyRepository));

            services.AddCaching(Configuration, _hostContext);

            services.AddScoped<IRaceService, RaceService>();
            services.AddScoped<IHorseService, HorseService>();
            services.AddScoped<IFeedingService, FeedingService>();
            services.AddScoped<IBreedingService, BreedingService>();
            services.AddScoped<ITrainingService, TrainingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TripleDerbyContext db)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Local"))
            {
                app.UseDeveloperExceptionPage();
            }

            db.Database.EnsureCreated();
            db.Database.Migrate();
            
            app.UseHttpsRedirection();

            app.UseSwaggerConfig();
            app.UseCorsConfig();
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (_hostContext.IsProduction())
            {
                app.UseHealthCheckConfig();
            }
        }
    }
}
