using System;
using System.Threading.Tasks;
using TripleDerby.Core.DTOs;

namespace TripleDerby.Core.Interfaces.Services
{
    public interface IHorseService
    {
        Task<HorseResult> Get(Guid id);
        Task<HorsesResult> GetAll(int pageIndex, int itemsPage);
    }
}
