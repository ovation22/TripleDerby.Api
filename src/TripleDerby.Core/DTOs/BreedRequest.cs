using System;

namespace TripleDerby.Core.DTOs
{
    public class BreedRequest
    {
        public Guid UserId { get; set; }

        public Guid SireId { get; set; }

        public Guid DamId { get; set; }
    }
}
