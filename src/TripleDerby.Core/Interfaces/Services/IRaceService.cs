using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripleDerby.Core.DTOs;

namespace TripleDerby.Core.Interfaces.Services
{
    public interface IRaceService
    {
        Task<RaceResult> Get(byte id);
        Task<IEnumerable<RacesResult>> GetAll();
        Task<RaceRunResult> Race(byte raceId, Guid horseId);
    }
}
