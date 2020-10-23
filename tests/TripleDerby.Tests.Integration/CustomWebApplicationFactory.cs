using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TripleDerby.Core.Entities;
using TripleDerby.Infrastructure.Data;

namespace TripleDerby.Tests.Integration
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<TripleDerbyContext>));

                services.Remove(descriptor);

                services.AddDbContextPool<TripleDerbyContext>(options =>
                {
                    options.UseInMemoryDatabase("TripleDerby");
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<TripleDerbyContext>();

                db.Database.EnsureCreated();

                InitializeDbForTests(db);
            });
        }

        private void InitializeDbForTests(TripleDerbyContext db)
        {
            db.Feedings.RemoveRange(db.Feedings);
            db.Feedings.Add(new Feeding
            {
                Id = 1,
                Name = "Apple",
                Description = "Apple"
            });
            db.SaveChanges();
        }
    }
}