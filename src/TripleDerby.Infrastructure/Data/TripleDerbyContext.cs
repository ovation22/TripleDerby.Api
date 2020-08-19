﻿using Microsoft.EntityFrameworkCore;
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
        public virtual DbSet<HorseStatistic> HorseStatistics { get; set; } = null!;
        public virtual DbSet<Race> Races { get; set; } = null!;
        public virtual DbSet<RaceRun> RaceRuns { get; set; } = null!;
        public virtual DbSet<RaceRunHorse> RaceRunHorses { get; set; } = null!;
        public virtual DbSet<RaceRunTick> RaceRunTicks { get; set; } = null!;
        public virtual DbSet<Surface> Surfaces { get; set; } = null!;
        public virtual DbSet<Track> Tracks { get; set; } = null!;
        public virtual DbSet<Training> Trainings { get; set; } = null!;

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

            modelBuilder.Entity<Race>()
                .Property(c => c.TrackId)
                .HasConversion<byte>();

            modelBuilder.Entity<Race>()
                .Property(c => c.SurfaceId)
                .HasConversion<byte>();
        }
    }
}