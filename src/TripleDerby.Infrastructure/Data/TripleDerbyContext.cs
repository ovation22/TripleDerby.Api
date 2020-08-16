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
        }
    }
}