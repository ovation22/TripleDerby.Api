using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using TripleDerby.Core.Cache;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Enums;
using TripleDerby.Core.Interfaces.Caching;
using TripleDerby.Core.Interfaces.Repositories;
using TripleDerby.Core.Interfaces.Services;

namespace TripleDerby.Core.Services
{
    public class BreedingService : IBreedingService
    {
        private readonly int _cacheExpirationMinutes;
        private readonly IDistributedCacheAdapter _cache;
        private readonly ITripleDerbyRepository _repository;

        public BreedingService(
            IDistributedCacheAdapter cache,
            ITripleDerbyRepository repository,
            IOptions<CacheConfig> cacheOptions
        )
        {
            _cache = cache;
            _repository = repository;
            _cacheExpirationMinutes = cacheOptions.Value.DefaultExpirationMinutes;
        }

        public async Task<IEnumerable<ParentHorse>> GetSires()
        {
            return await GetParentHorses(CacheKeys.FeaturedSires, true);
        }

        public async Task<Foal> Breed(Guid userId, Guid damId, Guid sireId)
        {
            var horse = new Horse
            {
                Name = "TODO",
                Color = Color.Bay,
                LegTypeId = 1,
                IsMale = true,
                SireId = sireId,
                DamId = damId,
                RaceStarts = 0,
                RaceWins = 0,
                RacePlaces = 0,
                RaceShows = 0,
                Earnings = 0,
                IsRetired = false,
                Parented = 0,
                OwnerId = userId
            };

            var speed = new HorseStatistic{ Statistic = Statistic.Speed };
            var stamina = new HorseStatistic { Statistic = Statistic.Stamina };
            var agility = new HorseStatistic { Statistic = Statistic.Agility };
            var happiness = new HorseStatistic { Statistic = Statistic.Happiness };
            var durability = new HorseStatistic { Statistic = Statistic.Durability };

            horse.Statistics.Add(speed);
            horse.Statistics.Add(stamina);
            horse.Statistics.Add(agility);
            horse.Statistics.Add(happiness);
            horse.Statistics.Add(durability);

            var foal = await _repository.Add(horse);

            return new Foal
            {
                Id = horse.Id,
                Name = foal.Name,
                Color = nameof(foal.Color),
                ColorId = (byte)foal.Color
            };
        }

        public async Task<IEnumerable<ParentHorse>> GetDams()
        {
            return await GetParentHorses(CacheKeys.FeaturedDams, false);
        }

        private async Task<IEnumerable<ParentHorse>> GetParentHorses(string cacheKey, bool isMale)
        {
            IEnumerable<ParentHorse> results;
            var cacheValue = await _cache.GetStringAsync(cacheKey);

            if (string.IsNullOrEmpty(cacheValue))
            {
                results = (await GetRandomHorses(isMale)).ToList();
                await SetCache(cacheKey, results);
            }
            else
            {
                results = JsonSerializer.Deserialize<List<ParentHorse>>(cacheValue);
            }

            return results;
        }

        private async Task SetCache(string cacheKey, IEnumerable<ParentHorse> results)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheExpirationMinutes)
            };

            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(results), options);
        }

        private async Task<IEnumerable<ParentHorse>> GetRandomHorses(bool isMale)
        {
            var speakers = await _repository.GetRandom<Horse>(x => x.IsMale == isMale, 10);

            var results = speakers.Select(x => new ParentHorse
            {
                Id = x.Id,
                Name = x.Name,
            });

            return results;
        }
    }
}
