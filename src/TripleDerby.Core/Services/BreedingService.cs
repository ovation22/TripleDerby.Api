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
using TripleDerby.Core.Interfaces.Utilities;

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

        public async Task<Foal> Breed(Guid userId, Guid damId, Guid sireId)
        {
            var dam = await _repository.Get<Horse>(x => x.DamId == damId, y => y.Color, z => z.Statistics);
            var sire = await _repository.Get<Horse>(x => x.SireId == sireId, y => y.Color, z => z.Statistics);

            var color = await GetRandomColor(sire.Color.IsSpecial, dam.Color.IsSpecial, true);
            var isMale = GetRandomGender();
            var legType = await GetRandomLegType();

            var horse = new Horse
            {
                Name = "TODO",
                ColorId = color.Id,
                LegTypeId = legType.Id,
                IsMale = isMale,
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

            horse.Statistics.ToList().AddRange(GenerateHorseStatistics(sire, dam));
            
            var foal = await _repository.Add(horse);

            return new Foal
            {
                Id = horse.Id,
                Name = foal.Name,
                Color = nameof(foal.Color),
                ColorId = 1
            };
        }

        public async Task<Color> GetRandomColor( bool isSireSpecial, bool isDamSpecial, bool includeSpecialColors)
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

            List<Color> colors = new List<Color>();

            if (includeSpecialColors)
            {
                colors.AddRange(await _repository.GetAll<Color>());
            }
            else
            {
                colors.AddRange(await _repository.GetAll<Color>(x => x.IsSpecial == false));
            }

            IEnumerable<Color> sortedColors = colors.OrderBy(x => _randomGenerator.Next(colors.Count * (x.IsSpecial ? x.Weight / multiplier : x.Weight)));

            return sortedColors.Take(1).SingleOrDefault();
        }

        public bool GetRandomGender()
        {
            return _randomGenerator.Next(1, 3) > 1;
        }

        public async Task<LegType> GetRandomLegType()
        {
            var legTypes = (List<LegType>) await _repository.GetAll<LegType>();
            IEnumerable<LegType> sortedLegTypes = legTypes.OrderBy(x => _randomGenerator.Next() * x.Weight);

            return sortedLegTypes.Take(1).SingleOrDefault();
        }

        public List<HorseStatistic> GenerateHorseStatistics(Horse sire, Horse dam)
        {
            List<HorseStatistic> foalStatistics = new List<HorseStatistic>();

            foreach (var statistic in Enum.GetValues(typeof(Statistic)).Cast<Statistic>().Where(x => x != Statistic.Happiness))
            {
                int punnettQuadrant = _randomGenerator.Next(1, 5);
                int whichGeneToPick = _randomGenerator.Next(1, 3);

                byte dominantPotential;
                byte recessivePotential;

                HorseStatistic sireStatistic = sire.Statistics.ToList().Single(x => x.Statistic == statistic);
                HorseStatistic damStatistic = dam.Statistics.ToList().Single(x => x.Statistic == statistic);

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
                    default:  //case 4
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
                    Statistic = sireStatistic.Statistic,
                    Actual = actual,
                    DominantPotential = dominantPotential,
                    RecessivePotential = recessivePotential
                };

                foalStatistics.Add(foalStatistic);
            }

            return foalStatistics;
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

            potential = (byte) (potential < 30 || potential > 95 ? 50 : potential);

            return potential;
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
            var horses = await _repository.GetRandom<Horse>(x => x.IsMale == isMale && x.IsRetired, 10);

            var results = horses.Select(x => new ParentHorse
            {
                Id = x.Id,
                Name = x.Name,
            });

            return results;
        }
    }
}
