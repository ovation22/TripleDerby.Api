using TripleDerby.Core.Enums;

namespace TripleDerby.Core.DTOs
{
    public class RacesResult
    {
        public byte Id { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public float Furlongs { get; set; }

        public SurfaceId SurfaceId { get; set; }

        public string Surface { get; set; } = default!;

        public TrackId TrackId { get; set; }

        public string Track { get; set; } = default!;
    }
}
