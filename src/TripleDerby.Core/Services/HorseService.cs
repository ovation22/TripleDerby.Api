using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TripleDerby.Core.Cache;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Interfaces.Caching;
using TripleDerby.Core.Interfaces.Repositories;
using TripleDerby.Core.Interfaces.Services;

namespace TripleDerby.Core.Services
{
    public class HorseService : IHorseService
    {
        private readonly int _cacheExpirationMinutes;
        private readonly ITripleDerbyRepository _repository;

        public HorseService(
            ITripleDerbyRepository repository,
            IOptions<CacheConfig> cacheOptions
        )
        {
            _repository = repository;
            _cacheExpirationMinutes = cacheOptions.Value.DefaultExpirationMinutes;
        }

        public async Task<HorseResult> Get(Guid id)
        {
            var horse = await _repository.Get<Horse>(x => x.Id == id);

            return new HorseResult
            {
                Id = horse.Id,
                Name = horse.Name
            };
        }

        public async Task<IEnumerable<HorsesResult>> GetAll()
        {
            var horses = await _repository.GetAll<Horse>();

            return horses.Select(x => new HorsesResult
            {
                Id = x.Id,
                Name = x.Name
            });
        }
    }
}
