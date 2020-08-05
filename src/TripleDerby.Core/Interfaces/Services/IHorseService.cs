using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripleDerby.Core.DTOs;

namespace TripleDerby.Core.Interfaces.Services
{
    public interface IHorseService
    {
        Task<HorseResult> Get(Guid id);
        Task<IEnumerable<HorsesResult>> GetAll();
    }
}
