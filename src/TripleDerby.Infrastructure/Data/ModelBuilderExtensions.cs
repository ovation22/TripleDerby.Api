using System;
using Microsoft.EntityFrameworkCore;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Enums;

namespace TripleDerby.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var adminId = new Guid("725C6768-7EAB-43B0-AA39-86F15E97824A");

            modelBuilder.Entity<User>().HasData(
                new User { Id = adminId, Username = "Admin", Email = "admin@triplederby.com", IsActive = true, IsAdmin = true }
            );

            modelBuilder.Entity<Color>().HasData(
                new Color { Id = 1, Name = "Gray", Description = "Color", Weight = 1, IsSpecial = false },
                new Color { Id = 2, Name = "Bay", Description = "Color", Weight = 1, IsSpecial = false },
                new Color { Id = 3, Name = "Seal Brown", Description = "Color", Weight = 1, IsSpecial = false },
                new Color { Id = 4, Name = "Chestnut", Description = "Color", Weight = 2, IsSpecial = false },
                new Color { Id = 5, Name = "Black", Description = "Color", Weight = 3, IsSpecial = false },
                new Color { Id = 6, Name = "Roan", Description = "Color", Weight = 4, IsSpecial = false },
                new Color { Id = 7, Name = "Palomino", Description = "Color", Weight = 5, IsSpecial = false },
                new Color { Id = 8, Name = "White", Description = "Color", Weight = 50, IsSpecial = false },
                new Color { Id = 9, Name = "Platinum", Description = "Color", Weight = 100, IsSpecial = false }
            );

            modelBuilder.Entity<Feeding>().HasData(
                new Feeding { Id = 1, Name = "Apple", Description = "Apple" }
            );

            modelBuilder.Entity<Statistic>().HasData(
                new Statistic { Id = StatisticId.Speed, Name = "Speed", Description = "Speed", IsGenetic = true },
                new Statistic { Id = StatisticId.Stamina, Name = "Stamina", Description = "Stamina", IsGenetic = true },
                new Statistic { Id = StatisticId.Agility, Name = "Agility", Description = "Agility", IsGenetic = true },
                new Statistic { Id = StatisticId.Durability, Name = "Durability", Description = "Durability", IsGenetic = true },
                new Statistic { Id = StatisticId.Happiness, Name = "Happiness", Description = "Happiness", IsGenetic = false }
            );

            modelBuilder.Entity<Training>().HasData(
                new Training { Id = 1, Name = "Sprint", Description = "Sprint" }
            );

            modelBuilder.Entity<Horse>().HasData(
                new Horse { Id = new Guid("210BB356-F8AE-4DBE-98F6-16DFA20CF930"), Name = "War Admiral", ColorId = 2, LegTypeId = LegTypeId.FrontRunner, IsMale = true, OwnerId = adminId, RaceStarts = 76, RaceWins = 73, RacePlace = 2, RaceShow = 0, Earnings = 43553, IsRetired = true, Parented = 0 },
                new Horse { Id = new Guid("2357E7CB-AE0F-4A36-9B64-1D544D14DB23"), Name = "Genuine Risk", ColorId = 3, LegTypeId = LegTypeId.LastSpurt, IsMale = false, OwnerId = adminId, RaceStarts = 19, RaceWins = 19, RacePlace = 0, RaceShow = 0, Earnings = 1066085, IsRetired = true, Parented = 0 }
            );

            modelBuilder.Entity<HorseStatistic>().HasData(
                new HorseStatistic { Id = new Guid("711C7215-22ED-4BBD-A926-FCEAF5B215A1"), HorseId = new Guid("210BB356-F8AE-4DBE-98F6-16DFA20CF930"), StatisticId = StatisticId.Speed, Actual = 70, DominantPotential = 70, RecessivePotential = 75 },
                new HorseStatistic { Id = new Guid("B125A994-100B-499B-B6D0-939D5ADD05A9"), HorseId = new Guid("210BB356-F8AE-4DBE-98F6-16DFA20CF930"), StatisticId = StatisticId.Stamina, Actual = 70, DominantPotential = 70, RecessivePotential = 80 },
                new HorseStatistic { Id = new Guid("E1407FE2-EA95-4C9A-9A29-BCD85AF63726"), HorseId = new Guid("210BB356-F8AE-4DBE-98F6-16DFA20CF930"), StatisticId = StatisticId.Agility, Actual = 70, DominantPotential = 75, RecessivePotential = 70 },
                new HorseStatistic { Id = new Guid("DB342410-7708-47C5-8788-3A46DD52E8AB"), HorseId = new Guid("210BB356-F8AE-4DBE-98F6-16DFA20CF930"), StatisticId = StatisticId.Durability, Actual = 70, DominantPotential = 75, RecessivePotential = 70 },
                new HorseStatistic { Id = new Guid("1F8D9D4D-2417-43FC-96E9-A7BE9DABEC52"), HorseId = new Guid("2357E7CB-AE0F-4A36-9B64-1D544D14DB23"), StatisticId = StatisticId.Speed, Actual = 71, DominantPotential = 74, RecessivePotential = 64 },
                new HorseStatistic { Id = new Guid("42D67B23-1471-4B00-81E6-E90FDD8E80C7"), HorseId = new Guid("2357E7CB-AE0F-4A36-9B64-1D544D14DB23"), StatisticId = StatisticId.Stamina, Actual = 74, DominantPotential = 81, RecessivePotential = 71 },
                new HorseStatistic { Id = new Guid("F51C705F-9AC2-4E18-91B1-FB30D527774C"), HorseId = new Guid("2357E7CB-AE0F-4A36-9B64-1D544D14DB23"), StatisticId = StatisticId.Agility, Actual = 61, DominantPotential = 64, RecessivePotential = 71 },
                new HorseStatistic { Id = new Guid("DB9C2292-7641-4C54-98A4-F095BBACC361"), HorseId = new Guid("2357E7CB-AE0F-4A36-9B64-1D544D14DB23"), StatisticId = StatisticId.Durability, Actual = 64, DominantPotential = 71, RecessivePotential = 61 }
            );

            modelBuilder.Entity<Condition>().HasData(
                new Condition { Id = ConditionId.Fast, Name = "Fast" }
            );

            modelBuilder.Entity<Surface>().HasData(
                new Surface { Id = SurfaceId.Dirt, Name = "Dirt" }
            );

            modelBuilder.Entity<Track>().HasData(
                new Track { Id = TrackId.Churchill, Name = "Churchill Downs" }
            );

            modelBuilder.Entity<Race>().HasData(
                new Race { Id = 1, Name = "Derby", Description = "Run for the Roses", SurfaceId = SurfaceId.Dirt, TrackId = TrackId.Churchill, Furlongs = 10 }
            );
        }
    }
}
