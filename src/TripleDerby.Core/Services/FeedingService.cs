using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Enums;
using TripleDerby.Core.Interfaces.Repositories;
using TripleDerby.Core.Interfaces.Services;
using TripleDerby.Core.Interfaces.Utilities;
using TripleDerby.Core.Specifications;

namespace TripleDerby.Core.Services
{
    public class FeedingService : IFeedingService
    {
        private readonly IRandomGenerator _randomGenerator;
        private readonly ITripleDerbyRepository _repository;

        public FeedingService(
            IRandomGenerator randomGenerator,
            ITripleDerbyRepository repository
        )
        {
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
            const string result = "Hooray!";

            await AffectHorseStatistic(horseId, StatisticId.Happiness, 0, 9, 0);

            var feedingSession = new FeedingSession
            {
                FeedingId = feedingId,
                HorseId = horseId,
                Result = result
            };

            await _repository.Add(feedingSession);

            return new FeedingSessionResult { Result = result };
        }

        private async Task AffectHorseStatistic(Guid horseId, StatisticId statisticId, int min, int max, int actualMin)
        {
            var stat = await _repository
                .Get(new HorseStatisticsSpecification(horseId, statisticId));

            stat.Actual = (byte) Math.Clamp(stat.Actual + _randomGenerator.Next(min, max),
                actualMin,
                stat.DominantPotential);
        }
    }
}
