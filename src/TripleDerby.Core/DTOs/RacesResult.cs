using TripleDerby.Core.Enums;

namespace TripleDerby.Core.DTOs
{
    public record RacesResult
    {
        public byte Id { get; init; }

        public string Name { get; init; } = default!;

        public string Description { get; init; } = default!;

        public decimal Furlongs { get; init; }

        public SurfaceId SurfaceId { get; init; }

        public string Surface { get; init; } = default!;

        public TrackId TrackId { get; init; }

        public string Track { get; init; } = default!;
    }
}
