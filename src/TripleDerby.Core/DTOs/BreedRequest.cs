using System;

namespace TripleDerby.Core.DTOs
{
    public record BreedRequest
    {
        public Guid UserId { get; init; }

        public Guid SireId { get; init; }

        public Guid DamId { get; init; }
    }
}
