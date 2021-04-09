using System.Collections.Generic;
using System.Threading.Tasks;
using TripleDerby.Core.DTOs;

namespace TripleDerby.Core.Interfaces.Services
{
    public interface IBreedingService
    {
        Task<IEnumerable<ParentHorse>> GetDams();
        Task<IEnumerable<ParentHorse>> GetSires();
        Task<Foal> Breed(BreedRequest request);
    }
}
