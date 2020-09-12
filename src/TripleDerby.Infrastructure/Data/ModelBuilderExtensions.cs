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
                new Feeding { Id = 1, Name = "Feed", Description = "Feed" }
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
                new Horse { Id = new Guid("210BB356-F8AE-4DBE-98F6-16DFA20CF930"), Name = "War Admiral", ColorId = 2, LegTypeId = LegTypeId.FrontRunner, IsMale = true, OwnerId = adminId, RaceStarts = 76, RaceWins = 73, RacePlace = 2, RaceShow = 0, Earnings = 43553, IsRetired = true, Parented = 0 }
            );
        }
    }
}
