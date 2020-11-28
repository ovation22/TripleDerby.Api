using System;
using System.Collections.Generic;

namespace TripleDerby.Core.DTOs
{
    public record HorsesResult
    {
        public PaginationInfo PaginationInfo { get; init; } = default!;
        
        public IEnumerable<Horse> Horses { get; init; } = new List<Horse>();

        public record Horse
        {
            public Guid Id { get; init; }

            public string Name { get; init; } = default!;

            public string Color { get; init; } = default!;

            public short RaceStarts { get; init; }

            public short RaceWins { get; init; }

            public short RacePlace { get; init; }

            public short RaceShow { get; init; }

            public int Earnings { get; init; }
        }
    }
}
