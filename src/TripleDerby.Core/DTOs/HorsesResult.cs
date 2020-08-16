using System;

namespace TripleDerby.Core.DTOs
{
    public class HorsesResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public string Color { get; set; } = default!;

        public short RaceStarts { get; set; }

        public short RaceWins { get; set; }

        public short RacePlace { get; set; }

        public short RaceShow { get; set; }

        public int Earnings { get; set; }

        public PaginationInfo PaginationInfo { get; set; } = default!;
    }
}
