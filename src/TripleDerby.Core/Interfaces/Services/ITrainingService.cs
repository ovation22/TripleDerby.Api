using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripleDerby.Core.DTOs;

namespace TripleDerby.Core.Interfaces.Services
{
    public interface ITrainingService
    {
        Task<TrainingResult> Get(byte id);
        Task<IEnumerable<TrainingsResult>> GetAll();
        Task<TrainingSessionResult> Train(byte trainingId, Guid horseId);
    }
}
