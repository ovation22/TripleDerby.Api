using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripleDerby.Core.DTOs;

namespace TripleDerby.Core.Interfaces.Services
{
    public interface IFeedingService
    {
        Task<FeedingResult> Get(byte id);
        Task<IEnumerable<FeedingsResult>> GetAll();
        Task<FeedingSessionResult> Feed(byte feedingId, Guid horseId);
    }
}
