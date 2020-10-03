using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class FeedingService : IFeedingService
    {
        private readonly ICacheManager _cache;
        private readonly IRandomGenerator _randomGenerator;
        private readonly ITripleDerbyRepository _repository;

        public FeedingService(
            ICacheManager cache,
            IRandomGenerator randomGenerator,
            ITripleDerbyRepository repository
        )
        {
            _cache = cache;
            _repository = repository;
            _randomGenerator = randomGenerator;
        }

        public async Task<FeedingResult> Get(byte id)
        {
            var feeding = await _repository.Get(new FeedingSpecification(id));

            return new FeedingResult
            {
                Id = feeding.Id,
                Name = feeding.Name,
                Description = feeding.Description
            };
        }

        public async Task<IEnumerable<FeedingsResult>> GetAll()
        {
            return await _cache.GetOrCreate(CacheKeys.Feedings, async () => await GetFeedings());
        }

        public async Task<IEnumerable<FeedingsResult>> GetFeedings()
        {
            var feedings = await _repository.GetAll<Feeding>();

            return feedings.Select(x => new FeedingsResult
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            });
        }

        public async Task<FeedingSessionResult> Feed(byte feedingId, Guid horseId)
        {
            const FeedResponse result = FeedResponse.Accepted;

            // Get Horse, with Stats, and with prior feedings of type
            // Save horse, with updated stats (Actual)
            // [] check feeding type past results to determine result, etc.
            var horse = await _repository.Get(new HorseWithHappinessAndPriorFeedingsSpecification(horseId, feedingId));
            var horseHappiness = horse.Statistics.Single(x => x.StatisticId == StatisticId.Happiness);

            horseHappiness.Actual = AffectHorseStatistic(horseHappiness, 0, 1, 0);

            var feedingSession = new FeedingSession
            {
                FeedingId = feedingId,
                Result = result
            };
            
            horse.FeedingSessions.Add(feedingSession);

            await _repository.Save();

            return new FeedingSessionResult { Result = result };
        }

        private byte AffectHorseStatistic(
            HorseStatistic stat,
            int min,
            int max,
            int actualMin
        )
        {
            return (byte) Math.Clamp(stat.Actual + _randomGenerator.Next(min, max),
                actualMin,
                stat.DominantPotential);
        }
    }
}
