using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Ardalis.Specification;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using TripleDerby.Core.Cache;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Enums;
using TripleDerby.Core.Interfaces.Caching;
using TripleDerby.Core.Interfaces.Repositories;
using TripleDerby.Core.Interfaces.Services;
using TripleDerby.Core.Interfaces.Utilities;
using TripleDerby.Core.Specifications;

namespace TripleDerby.Core.Services
{
    public class BreedingService : IBreedingService
    {
        private readonly int _cacheExpirationMinutes;
        private readonly IDistributedCacheAdapter _cache;
        private readonly IRandomGenerator _randomGenerator;
        private readonly ITripleDerbyRepository _repository;

        public BreedingService(
            IDistributedCacheAdapter cache,
            ITripleDerbyRepository repository,
            IRandomGenerator randomGenerator,
            IOptions<CacheConfig> cacheOptions
        )
        {
            _cache = cache;
            _repository = repository;
            _randomGenerator = randomGenerator;
            _cacheExpirationMinutes = cacheOptions.Value.DefaultExpirationMinutes;
        }

        public async Task<IEnumerable<ParentHorse>> GetDams()
        {
            return await GetParentHorses(CacheKeys.FeaturedDams, false);
        }

        public async Task<IEnumerable<ParentHorse>> GetSires()
        {
            return await GetParentHorses(CacheKeys.FeaturedSires, true);
        }

        public async Task<Foal> Breed(BreedRequest request)
        {
            var dam = await _repository.Get(new ParentHorseSpecification(request.DamId));
            var sire = await _repository.Get(new ParentHorseSpecification(request.SireId));

            var isMale = GetRandomGender();
            var legTypeId = GetRandomLegType();
            var color = await GetRandomColor(sire.Color.IsSpecial, dam.Color.IsSpecial, true);
            var statistics = GenerateHorseStatistics(sire.Statistics, dam.Statistics);

            var horse = new Horse
            {
                Name = "TODO",
                ColorId = color.Id,
                LegTypeId = legTypeId,
                IsMale = isMale,
                SireId = request.SireId,
                DamId = request.DamId,
                RaceStarts = 0,
                RaceWins = 0,
                RacePlace = 0,
                RaceShow = 0,
                Earnings = 0,
                IsRetired = false,
                Parented = 0,
                OwnerId = request.UserId,
                Statistics = statistics
            };

            dam.Parented += 1;
            sire.Parented += 1;
            
            var foal = _repository.Add(horse);
            await _repository.Save();

            return new Foal
            {
                Id = horse.Id,
                Name = foal.Name,
                Color = foal.Color.Name,
                ColorId = foal.ColorId
            };
        }

        public bool GetRandomGender()
        {
            return _randomGenerator.Next(1, 3) > 1;
        }

        public LegTypeId GetRandomLegType()
        {
            var legTypes = Enum.GetValues(typeof(LegTypeId)).Cast<LegTypeId>().ToList();
            var random = _randomGenerator.Next(1, legTypes.Count + 1);

            return legTypes.Find(legType => (LegTypeId)random == legType);
        }

        public async Task<Color> GetRandomColor(bool isSireSpecial, bool isDamSpecial, bool includeSpecialColors)
        {
            var multiplier = 1;

            if (isSireSpecial && !isDamSpecial)
            {
                multiplier = 10;
            }
            else if (!isSireSpecial && isDamSpecial)
            {
                multiplier = 25;
            }
            else if (isSireSpecial)
            {
                multiplier = 50;
            }

            var colors = (await _repository.GetAll<Color>()).ToList();

            IEnumerable<Color> sortedColors = colors
                .Where(x => includeSpecialColors || x.IsSpecial == false)
                .OrderBy(x => 
                    _randomGenerator
                        .Next(colors.Count * (x.IsSpecial ? x.Weight / multiplier : x.Weight)));

            return sortedColors.Take(1).Single();
        }

        public List<HorseStatistic> GenerateHorseStatistics(ICollection<HorseStatistic> sireStats, ICollection<HorseStatistic> damStats)
        {
            List<HorseStatistic> foalStatistics =
                new List<HorseStatistic> {new HorseStatistic {StatisticId = StatisticId.Happiness, DominantPotential = 100}};
            
            foalStatistics
                .AddRange(Enum.GetValues(typeof(StatisticId))
                .Cast<StatisticId>()
                .Where(x => x != StatisticId.Happiness)
                .Select(statistic => GenerateHorseStatistic(sireStats, damStats, statistic)));

            return foalStatistics;
        }

        private HorseStatistic GenerateHorseStatistic(IEnumerable<HorseStatistic> sireStats, IEnumerable<HorseStatistic> damStats, StatisticId statistic)
        {
            int punnettQuadrant = _randomGenerator.Next(1, 5);
            int whichGeneToPick = _randomGenerator.Next(1, 3);

            byte dominantPotential;
            byte recessivePotential;

            HorseStatistic sireStatistic = sireStats.Single(x => x.StatisticId == statistic);
            HorseStatistic damStatistic = damStats.Single(x => x.StatisticId == statistic);

            switch (punnettQuadrant)
            {
                case 1:
                    if (whichGeneToPick == 1)
                    {
                        dominantPotential = sireStatistic.DominantPotential;
                        recessivePotential = damStatistic.RecessivePotential;
                    }
                    else
                    {
                        dominantPotential = damStatistic.DominantPotential;
                        recessivePotential = sireStatistic.RecessivePotential;
                    }

                    break;
                case 2:
                    dominantPotential = sireStatistic.DominantPotential;
                    recessivePotential = damStatistic.RecessivePotential;
                    break;
                case 3:
                    dominantPotential = damStatistic.DominantPotential;
                    recessivePotential = sireStatistic.RecessivePotential;
                    break;
                default: //case 4
                    if (whichGeneToPick == 1)
                    {
                        dominantPotential = sireStatistic.DominantPotential;
                        recessivePotential = damStatistic.RecessivePotential;
                    }
                    else
                    {
                        dominantPotential = damStatistic.DominantPotential;
                        recessivePotential = sireStatistic.RecessivePotential;
                    }

                    break;
            }

            dominantPotential = MutatePotentialGene(dominantPotential);
            recessivePotential = MutatePotentialGene(recessivePotential);
            byte actual = (byte) _randomGenerator.Next(dominantPotential / 3, dominantPotential / 2);

            var foalStatistic = new HorseStatistic
            {
                StatisticId = sireStatistic.StatisticId,
                Actual = actual,
                DominantPotential = dominantPotential,
                RecessivePotential = recessivePotential
            };
            return foalStatistic;
        }

        public byte MutatePotentialGene(byte potential)
        {
            byte mutationMultiplier = (byte) _randomGenerator.Next(1, 101);
            int mutationLowerBound;
            byte mutationUpperBound;

            switch (mutationMultiplier)
            {
                case 1:
                    mutationLowerBound = 0;
                    mutationUpperBound = 16;
                    break;
                case 2:
                    mutationLowerBound = -15;
                    mutationUpperBound = 1;
                    break;
                default:
                    mutationLowerBound = -5;
                    mutationUpperBound = 6;
                    break;
            }

            potential = (byte) (potential + _randomGenerator.Next(mutationLowerBound, mutationUpperBound));

            potential = (byte) (potential is < 30 or > 95 ? 50 : potential);

            return potential;
        }

        private async Task<IEnumerable<ParentHorse>> GetParentHorses(string cacheKey, bool isMale)
        {
            IEnumerable<ParentHorse> results;
            var cacheValue = await _cache.GetStringAsync(cacheKey);

            if (string.IsNullOrEmpty(cacheValue))
            {
                results = (await GetRandomHorses(new HorseRandomRetiredSpecification(isMale))).ToList();

                await SetCache(cacheKey, results);
            }
            else
            {
                results = JsonSerializer.Deserialize<List<ParentHorse>>(cacheValue)!;
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

        private async Task<IEnumerable<ParentHorse>> GetRandomHorses(ISpecification<Horse> spec)
        {
            var horses = await _repository.List(spec);

            var results = horses.Select(x => new ParentHorse
            {
                Id = x.Id,
                Name = x.Name,
                Color = x.Color.Name,
                Earnings = x.Earnings,
                RacePlace = x.RacePlace,
                RaceShow = x.RaceShow,
                RaceStarts = x.RaceStarts,
                RaceWins = x.RaceWins
            });

            return results;
        }
    }
}
