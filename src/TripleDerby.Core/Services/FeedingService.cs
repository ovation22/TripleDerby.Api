using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Interfaces.Repositories;
using TripleDerby.Core.Interfaces.Services;
using TripleDerby.Core.Specifications;

namespace TripleDerby.Core.Services
{
    public class FeedingService : IFeedingService
    {
        private readonly ITripleDerbyRepository _repository;

        public FeedingService(
            ITripleDerbyRepository repository
        )
        {
            _repository = repository;
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
            var feedingSession = new FeedingSession
            {
                FeedingId = feedingId,
                HorseId = horseId,
                Result = result
            };

            await _repository.Add(feedingSession);

            return new FeedingSessionResult { Result = result };
        }
    }
}
