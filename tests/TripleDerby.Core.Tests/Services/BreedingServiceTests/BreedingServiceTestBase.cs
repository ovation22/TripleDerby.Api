using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using Moq;
using TripleDerby.Core.Cache;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Enums;
using TripleDerby.Core.Interfaces.Caching;
using TripleDerby.Core.Interfaces.Repositories;
using TripleDerby.Core.Interfaces.Utilities;
using TripleDerby.Core.Services;

namespace TripleDerby.Core.Tests.Services.BreedingServiceTests
{
    public class BreedingServiceTestBase
    {
        protected readonly Guid DamId;
        protected readonly Guid SireId;
        protected internal BreedingService Service;
        protected internal Mock<IDistributedCacheAdapter> Cache;
        protected internal Mock<IRandomGenerator> RandomGenerator;
        protected internal Mock<ITripleDerbyRepository> Repository;
        protected readonly Mock<IOptions<CacheConfig>> CacheOptions;

        public BreedingServiceTestBase()
        {
            Cache = new Mock<IDistributedCacheAdapter>();
            RandomGenerator = new Mock<IRandomGenerator>();
            Repository = new Mock<ITripleDerbyRepository>();
            CacheOptions = new Mock<IOptions<CacheConfig>>();

            CacheOptions.Setup(x => x.Value).Returns(() => new CacheConfig { DefaultExpirationMinutes = 2 });

            DamId = new Guid("7B48977C-754D-4463-B811-66DFCF5B4487");
            SireId = new Guid("FF55C438-DA12-48BC-A9D2-A6924335C8E6");

            var color = new Color { Id = 1, IsSpecial = false, Weight = 1 };
            var dam = new Horse
            {
                Id = DamId,
                Color = color,
                Statistics = new Collection<HorseStatistic>
                {
                    new HorseStatistic{StatisticId = StatisticId.Agility},
                    new HorseStatistic{StatisticId = StatisticId.Durability},
                    new HorseStatistic{StatisticId = StatisticId.Stamina},
                    new HorseStatistic{StatisticId = StatisticId.Speed}
                }
            };
            var sire = new Horse
            {
                Id = SireId,
                Color = color,
                Statistics = new Collection<HorseStatistic>
                {
                    new HorseStatistic{StatisticId = StatisticId.Agility},
                    new HorseStatistic{StatisticId = StatisticId.Durability},
                    new HorseStatistic{StatisticId = StatisticId.Stamina},
                    new HorseStatistic{StatisticId = StatisticId.Speed}
                }
            };

            Repository.Setup(x => x.GetAll<Color>())
                .ReturnsAsync(new List<Color> { color });

            Repository.SetupSequence(x => x.Get(It.IsAny<Expression<Func<Horse, bool>>>(), It.IsAny<Expression<Func<Horse, object>>[]>()))
                .ReturnsAsync(dam)
                .ReturnsAsync(sire);

            Service = new BreedingService(Cache.Object, Repository.Object, RandomGenerator.Object, CacheOptions.Object);
        }
    }
}
