using Microsoft.EntityFrameworkCore;
using TripleDerby.Core.Entities;

namespace TripleDerby.Infrastructure.Data
{
    public class TripleDerbyContext : DbContext
    {
        public TripleDerbyContext(DbContextOptions<TripleDerbyContext> options) : base(options)
        {
        }

        public virtual DbSet<Color> Colors { get; set; } = null!;
        public virtual DbSet<Horse> Horses { get; set; } = null!;
        public virtual DbSet<LegType> LegTypes { get; set; } = null!;
        public virtual DbSet<HorseStatistic> HorseStatistics { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HorseStatistic>()
                .Property(c => c.StatisticId)
                .HasConversion<byte>();
        }
    }
}