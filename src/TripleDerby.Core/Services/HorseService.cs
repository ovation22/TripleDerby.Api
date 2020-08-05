using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Entities;
using TripleDerby.Core.Interfaces.Repositories;
using TripleDerby.Core.Interfaces.Services;

namespace TripleDerby.Core.Services
{
    public class HorseService : IHorseService
    {
        private readonly ITripleDerbyRepository _repository;

        public HorseService(
            ITripleDerbyRepository repository
        )
        {
            _repository = repository;
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
