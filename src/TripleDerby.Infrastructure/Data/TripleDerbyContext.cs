using Microsoft.EntityFrameworkCore;
using TripleDerby.Core.Entities;

namespace TripleDerby.Infrastructure.Data
{
    public class TripleDerbyContext : DbContext
    {
        public TripleDerbyContext(DbContextOptions<TripleDerbyContext> options) : base(options)
        {
        }

        public virtual DbSet<Horse> Horses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Horse>()
                .Property(c => c.Color)
                .HasConversion<byte>();

            modelBuilder.Entity<HorseStatistic>()
                .Property(c => c.Statistic)
                .HasConversion<byte>();
        }
    }
}