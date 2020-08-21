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
    public class TrainingService : ITrainingService
    {
        private readonly ITripleDerbyRepository _repository;

        public TrainingService(
            ITripleDerbyRepository repository
        )
        {
            _repository = repository;
        }

        public async Task<TrainingResult> Get(byte id)
        {
            var training = await _repository.Get(new TrainingSpecification(id));

            return new TrainingResult
            {
                Id = training.Id,
                Name = training.Name,
                Description = training.Description
            };
        }

        public async Task<IEnumerable<TrainingsResult>> GetAll()
        {
            var trainings = await _repository.GetAll<Training>();

            return trainings.Select(x => new TrainingsResult
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            });
        }

        public async Task<TrainingSessionResult> Train(byte trainingId, Guid horseId)
        {
            const string result = "Hooray!";
            var trainingSession = new TrainingSession
            {
                TrainingId = trainingId,
                HorseId = horseId,
                Result = result
            };

            await _repository.Add(trainingSession);

            return new TrainingSessionResult { Result = result };
        }
    }
}
