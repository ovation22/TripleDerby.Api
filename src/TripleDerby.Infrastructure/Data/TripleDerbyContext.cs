using Microsoft.EntityFrameworkCore;
using TripleDerby.Core.Entities;

namespace TripleDerby.Infrastructure.Data
{
    public sealed class TripleDerbyContext : DbContext
    {
        public TripleDerbyContext(DbContextOptions<TripleDerbyContext> options) : base(options)
        {
        }

        public DbSet<Color> Colors { get; set; } = null!;
        public DbSet<Feeding> Feedings { get; set; } = null!;
        public DbSet<FeedingSession> FeedingSession { set; get; } = null!;
        public DbSet<Horse> Horses { get; set; } = null!;
        public DbSet<HorseStatistic> HorseStatistics { get; set; } = null!;
        public DbSet<Race> Races { get; set; } = null!;
        public DbSet<RaceRun> RaceRuns { get; set; } = null!;
        public DbSet<RaceRunHorse> RaceRunHorses { get; set; } = null!;
        public DbSet<RaceRunTick> RaceRunTicks { get; set; } = null!;
        public DbSet<RaceRunTickHorse> RaceRunTickHorses { get; set; } = null!;
        public DbSet<Surface> Surfaces { get; set; } = null!;
        public DbSet<Track> Tracks { get; set; } = null!;
        public DbSet<Training> Trainings { get; set; } = null!;
        public DbSet<TrainingSession> TrainingSessions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HorseStatistic>()
                .Property(c => c.StatisticId)
                .HasConversion<byte>();

            modelBuilder.Entity<Horse>()
                .Property(c => c.LegTypeId)
                .HasConversion<byte>();

            modelBuilder.Entity<Horse>()
                .HasOne(x => x.Sire)
                .WithMany();

            modelBuilder.Entity<Horse>()
                .HasOne(x => x.Dam)
                .WithMany();

            modelBuilder.Entity<Track>()
                .Property(c => c.Id)
                .HasConversion<byte>();

            modelBuilder.Entity<Surface>()
                .Property(c => c.Id)
                .HasConversion<byte>();

            modelBuilder.Entity<Race>()
                .Property(c => c.TrackId)
                .HasConversion<byte>();

            modelBuilder.Entity<Race>()
                .Property(c => c.SurfaceId)
                .HasConversion<byte>();
        }
    }
}